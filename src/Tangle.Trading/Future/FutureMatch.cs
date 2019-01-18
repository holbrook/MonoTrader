using System;
using Tangle.Trading.Base;

namespace Tangle.Trading.Future
{
    public class FutureMatch:MatchBase
    {
        /*

knockPrice  成交价格        撤单成交不返回此字段
knockamt    成交金额        
knockQty    成交数量        撤单成交不返回此字段
totalwithdrawqty    撤单成交数量      
orderqty    委托数量        
orderprice  委托价格        
serialnum   流水号     
contractNum 合同序号                    
acctId  资金帐号        
regId   股东代码        
knockTime   成交时间        
knockCode   成交编号        
reckoningamt    资金发生数       
fullknockamt    全价成交金额      
accuredinterestamt  应计利息金额      
accuredinterest 应计利息        
returnflag  处理结果        " 0：委托确认成功 1：委托确认非法 2：成交  3：交易所撤单成功 4：根网系统内部撤单成功 
 5：交易所撤单非法"
occurTime   发生时间        
ordertime   委托时间        
batchnum    委托批号        
forderserialnum 委托记录的序（流水）号     
f_offsetflag    开平标志        
closepnl    平仓盈亏        
openusedmarginamt   今开占用保证金     
offsetmarginamt 平仓释放保证金     
bsflag  买卖方向        
ordersource 订单来源        
ordersourceid   委托标识ID      
internaloffsetflag  内部开平标志      
premium 期权权利金收支     
commision   手续费     
f_hedgeflag 投保标记        
orderstatus 委托状态        
coveredflag 备兑标签(0-备兑,1-非备兑)        
f_matchcondition    有效期类型       
actionflag  报单操作类型      
businessmark    交易业务类型      
tradefrozenamt  交易冻结        
ownertype   订单所有类型      
totalknockqty   总成交数量       
totalknockamt   总成交金额       
appserverip APPSVRID        
actiontime  修改时间        
exchErrorCode   交易所错误编码     
memo    错误信息        
*/
    }
}
