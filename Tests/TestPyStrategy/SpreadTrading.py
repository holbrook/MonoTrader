# 假设两只股票的价比ratioAB(priceA/priceB) 有一个合理范围 (ratio_min, ratio_max)。
# 当超出这个范围时，只配置单一股票； 当在这个范围内时，持仓市值按比例配置。
# 也就是说，当比例从ratio_min 变化到 ratio_max 时，  股票A的占比从100% 变化为 0%。

# 目标计算方法：VA:  Value of A
# VA/(VA+VB):     100--0
# ratio(PA/PB):   min--max
# 

# 为了规避小幅波动，设定一个最小调仓金额 transfer_amt
# 即 需要调仓的金额超过该阈值时才调仓。

# 每次事件时，查询持仓并计算市值。并根据行情数据得到价比，然后计算出目标市值。
# 然后比对持仓市值和目标市值的差。如果超过阈值，则进行调仓

# wsd_data['601398.SH']/wsd_data['601939.SH']
#count    264.000000
#mean       0.803562
#std        0.017506
#min        0.763507
#25%        0.793904
#50%        0.801075
#75%        0.807394
#max        0.871345



def initialize(context):# 定义初始化函数
    # 策略参数
    context.ratio_min = 0.793904
    context.ratio_max = 0.807394
    context.transfer_amt = 20000
    
    # 运行变量
    context.available_fund = 0.0
    context.amountA = 0.0
    context.amountB = 0.0
    
    # 回测参数
    context.capital = 1000000 # 回测的初始资金
    context.securities = ['601398.SH','601939.SH'] # 回测标的. 工商银行(参考价5)
    context.start_date = '20180101' #"20150123" # 回测开始时间
    context.end_date = "20181210" # 回测结束时间
    context.period = 'd' # 策略运行周期, 'd' 代表日, 'm'代表分钟
    print("hello")

def update_position(context):
    # 计算现状市值占比 wA0, wB0
    position=bkt.query_position() #查询当前持仓
    if(None!=position):
        if(context.securities[0] in position.get_field('code')):
            context.amountA = position[context.securities[0]]['amount']
        if(context.securities[1] in position.get_field('code')):
            context.amountB = position[context.securities[1]]['amount']
            
    
def handle_data(bar_datetime, context, bar_data):#定义策略函数
    # 根据比价计算目标市值占比 wA, wB
    context.priceA = bar_data[context.securities[0]]['close']
    context.priceB = bar_data[context.securities[1]]['close']
    if(context.priceA==0 or context.priceB==0):
        print("未取到价格，不执行操作")
        return
    
    update_position(context)
    
    targetA = (context.ratio_max-context.priceA/context.priceB)/(context.ratio_max - context.ratio_min)
    if(targetA>1.0):
        targetA = 1.0
    if(targetA<0):
        targetA = 0.0
    print('目标市值比：%s %f, %s %f' % (context.securities[0], targetA, context.securities[1], (1-targetA)))
    
    # 如果 持仓都为0： 建仓（用全部资金）    
    if(context.amountA + context.amountB == 0):
        print('空仓，开始建仓')
        res = bkt.order_target_value(context.securities[0],context.capital*targetA , price='close', volume_check=False)
        res = bkt.order_target_value(context.securities[1],context.capital*(1-targetA) , price='close', volume_check=False)
        return
         
    #卖 A 买 B
    diffA = context.amountA - targetA*(context.amountA + context.amountB)
    if(diffA >= context.transfer_amt):
        bkt.order_value(context.securities[0],diffA,'sell',price='close', volume_check=False)
        context.available_fund = bkt.query_capital()[0]['available_fund']
        print('卖出A后可用资金：%f' % (context.available_fund))
        bkt.order_value(context.securities[1],context.available_fund,'buy',price='close', volume_check=False)
        return

    #卖 B 买 A
    diffB = context.amountB - (1-targetA)*(context.amountA + context.amountB)
    if(diffB >= context.transfer_amt):
        bkt.order_value(context.securities[1],diffB,'sell',price='close', volume_check=False)
        context.available_fund = bkt.query_capital()[0]['available_fund']
        print('卖出B后可用资金：%f' % (context.available_fund))
        bkt.order_value(context.securities[0],context.available_fund,'buy',price='close', volume_check=False)
        return

                
# bkt = BackTest(init_func = initialize, handle_data_func=handle_data) #实例化回测对象
# res = bkt.run(show_progress=True) #调用run()函数开始回测,show_progress可用于指定是否显示回测净值曲线图
# nav_df=bkt.summary('nav') #获取回测结果



