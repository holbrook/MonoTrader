using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangleTrading.Adapter;

namespace TangleTrading.RootNetAdapter
{
    public class RootNetFeeder : RootNetBase, IFeeder
    {
        private List<string> Codes { get; set; }

        public RootNetFeeder():base()
        {
            Codes = new List<string>();
        }

        public void Subscribe(string code, string type)
        {
            if (!Codes.Contains(code))
                Codes.Add(code);            
        }

        public void UnSubscribe(string code, string type)
        {
            if (Codes.Contains(code))
                Codes.Remove(code);
        }

        //轮询
        public void Go()
        {

        }

        public ExecuteStatus GetStockTick(string code)
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00100010");   //取证券行情
            oPackage.SetFlags(0);

            oPackage.SetValue(0, "recordCnt", "1");        // 返回记录数

            oPackage.SetValue(1, "optId", commonParams.optId);                  //柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", commonParams.optMode);              //委托方式
            //oPackage.SetValue(1, "permitMac", commonParams.permitMac);          //登录Mac地址
            
            
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息


            oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(code)); //市场代码 Y
            oPackage.SetValue(1, "stkId", Tangle2RootNet.OrderbookID2stkId(code));     //证券代码    Y 转股回售、权证行权时不使用这个字段


            //oPackage.SetValue(1, "maxRowNum", "500");                   //每次返回的最大记录数  Y 取值范围：1～500
            //oPackage.SetValue(1, "packNum", "1");                   //查询序号    Y 首次查询时为1，查下一页时递加1


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            string s = oPackage.GetValue(0, "successflg");

            try
            {
                
                ret.Code = int.Parse(s);


                if (0 != ret.Code)
                    ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

                else
                {
                    ret.Message = "调用成功";

                    Stock.Tick tick = new Stock.Tick();
                    tick.OrderbookID = oPackage.GetValue(1, "stkId") + "." + RootNet2Tangle.TransExchID(oPackage.GetValue(1, "exchId"));
                    tick.InstrumentName = oPackage.GetValue(1, "stkName");
                    tick.Open = decimal.Parse(oPackage.GetValue(1, "openPrice"));     //// 今日开盘
                    tick.High = decimal.Parse(oPackage.GetValue(1, "highPrice"));     // 今日最高
                    tick.Low = decimal.Parse(oPackage.GetValue(1, "lowPrice"));      // 今日最低
                    tick.PrevClose = decimal.Parse(oPackage.GetValue(1, "closePrice"));      // 昨日收盘
                    tick.LastPrice = decimal.Parse(oPackage.GetValue(1, "newPrice"));      // 最近成交
                    tick.Volume = int.Parse(oPackage.GetValue(1, "knockQty"));      // 成交数量

                    tick.AskPrices = new decimal[] {
                        decimal.Parse(oPackage.GetValue(1, "sell1")),       //卖价
                        decimal.Parse(oPackage.GetValue(1, "sell2")),
                        decimal.Parse(oPackage.GetValue(1, "sell3")),
                        decimal.Parse(oPackage.GetValue(1, "sell4")),
                        decimal.Parse(oPackage.GetValue(1, "sell5"))
                    };

                    tick.BidPrices = new decimal[] {
                        decimal.Parse(oPackage.GetValue(1, "buy1")),       //买价
                        decimal.Parse(oPackage.GetValue(1, "buy2")),
                        decimal.Parse(oPackage.GetValue(1, "buy3")),
                        decimal.Parse(oPackage.GetValue(1, "buy4")),
                        decimal.Parse(oPackage.GetValue(1, "buy5"))
                    };

                    tick.AskVolumes = new int[]
                    {
                        int.Parse(oPackage.GetValue(1, "sellAmt1")),       //卖量
                        int.Parse(oPackage.GetValue(1, "sellAmt2")),
                        int.Parse(oPackage.GetValue(1, "sellAmt3")),
                        int.Parse(oPackage.GetValue(1, "sellAmt4")),
                        int.Parse(oPackage.GetValue(1, "sellAmt5"))
                    };

                    tick.BidVolumes = new int[]
                    {
                        int.Parse(oPackage.GetValue(1, "buyAmt1")),       //买量
                        int.Parse(oPackage.GetValue(1, "buyAmt2")),
                        int.Parse(oPackage.GetValue(1, "buyAmt3")),
                        int.Parse(oPackage.GetValue(1, "buyAmt4")),
                        int.Parse(oPackage.GetValue(1, "buyAmt5"))
                    };

                    ret.Data = tick;
                }
            }
            catch (Exception e)
            {
                ret.Code = -1;
                ret.Message = e.Message;               
            }
            return ret;

            //stkType 证券类别
            //tradeType 交易类型
            //stkParValue 股票面值
            //maxorderQty 委托数量上限
            //minBuyStkQty 买入下限
            //minSellStkQty 卖出下限
            //maxOrderPrice 价格上限
            //minOrderPrice 价格下限
            //upPercent 涨幅比例
            //downPercent 跌幅比例
            //orderUnit 委托单位        0 - 股 / 张，1 - 手
            //qtyPerhand 每手股数
            //orderPriceUnit 价格档位
            //bsPermit 买/ 卖 / 双向标志控制
            //orderPriceFlag 价格标志        0 - 客户委托，1 - 系统定价


            //compositIndex 成分指数

            
            //accuredInterest 国债应计利息
            //optionType 权证类型
            //settlementType 权证结算方式
            //strikeStyle 行权方式
            //strikeRate 行权比例
            //expirationDate 权证到期日
            //strikestkid 行权代码
            //optionstkid 权证代码
            //basicstkid 标的证券代码
            //strikeprice 行权价格
            //settlementprice 结算价格
            //stklevel 证券级别
            //remainTradeDays 剩余交易天数
            //tradeCurrencyId 交易币种        港股通有效，返回为02 - 港币
            //exchRate 交易日间参考中间价汇率     港股通有效
            //str1    价格区间1 港股通市场证券才提供，比最新价区间价格高的后一价格区间，用“XXX - XXX”描述价格区间
            //price1  价格区间1的价格档位 港股通市场证券才提供，比最新价区间价格高的后一价格区间的价格档位
            //str2    价格区间2 港股通市场证券才提供，最新价所属的价格区间
            //price2  价格区间2的价格档位 港股通市场证券才提供，最新价所属的价格区间的价格档位
            //str3    价格区间3 港股通市场证券才提供，比最新价区间价格低的前一价格区间
            //price3  价格区间3的价格档位 港股通市场证券才提供，比最新价区间价格低的前一价格区间的价格档位
            //stkOrderStatus  证券状态        "TRD-连续交易、VOLIP_M-可恢复熔断、VOLIP_N-不可恢复熔断、PAUSE-临时停牌、STOP-停牌、
            //RISELIMIT - 涨停、DECLINELIMIT - 跌停"
            //stkIssueType 新股发行方式      1 - 市值配售方式、2 - 上网定价方式、4 - 增发定价方式、5 - 增发竞价方式、6 - LOF / ETF连续认购方式

        }

        //TODO:
        //stkIdList   合约代码列表 N(允许多选查询，使用^ 分隔）
        public ExecuteStatus GetFutureTick(string code)
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("20100010");   // 获得期货合约信息
            oPackage.SetFlags(0);

            oPackage.SetValue(0, "recordCnt", "1");        // 返回记录数

            oPackage.SetValue(1, "optId", commonParams.optId);                  //柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", commonParams.optMode);              //委托方式
                                                                                //oPackage.SetValue(1, "permitMac", commonParams.permitMac);          //登录Mac地址


            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息


            oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(code)); //市场代码 Y
            oPackage.SetValue(1, "stkId", Tangle2RootNet.OrderbookID2stkId(code));     //证券代码    Y 转股回售、权证行权时不使用这个字段


            //oPackage.SetValue(1, "maxRowNum", "500");                   //每次返回的最大记录数  Y 取值范围：1～500
            //oPackage.SetValue(1, "packNum", "1");                   //查询序号    Y 首次查询时为1，查下一页时递加1


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();

            string s = oPackage.GetValue(0, "successflg");


            try { 
                
                ret.Code = int.Parse(s);

                if (0 != ret.Code)
                    ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

                else
                {
                    ret.Message = "调用成功";

                    Future.Tick tick = new Future.Tick();
                    tick.OrderbookID = oPackage.GetValue(1, "stkId") + "." + RootNet2Tangle.TransExchID(oPackage.GetValue(1, "exchId"));
                    tick.InstrumentName = oPackage.GetValue(1, "stkName");
                    tick.Open = decimal.Parse(oPackage.GetValue(1, "openPrice"));     //// 今日开盘
                    tick.High = decimal.Parse(oPackage.GetValue(1, "highestPrice"));     // 今日最高
                    tick.Low = decimal.Parse(oPackage.GetValue(1, "lowestPrice"));      // 今日最低
                    tick.PrevClose = decimal.Parse(oPackage.GetValue(1, "preClosePrice"));      // 昨日收盘
                    tick.LastPrice = decimal.Parse(oPackage.GetValue(1, "newPrice"));      // 最近成交
                    //tick.Volume = int.Parse(oPackage.GetValue(1, "exchTotalKnockQty"));       // 总成交数量
                    //tick.Timestamp = DateTime.Parse(oPackage.GetValue(1, "lastModifyTime"));    //
                    Console.WriteLine(tick.Timestamp);


                    tick.AskPrices = new decimal[] {
                        decimal.Parse(oPackage.GetValue(1, "sell1"))       //卖价                        
                    };

                    tick.BidPrices = new decimal[] {
                        decimal.Parse(oPackage.GetValue(1, "buy1"))       //买价
                    };

                    tick.AskVolumes = new int[]
                    {
                        int.Parse(oPackage.GetValue(1, "sellAmt1"))       //卖量
                    };

                    tick.BidVolumes = new int[]
                    {
                        int.Parse(oPackage.GetValue(1, "buyAmt1"))       //买量
                    };

                    ret.Data = tick;
                }
            }
            catch (Exception e)
            {
                ret.Code = -1;
                ret.Message = e.Message;               
            }

            return ret;


            
           


            //参数名 说明 备注
            //settleGrp 结算组代码
            //settleID 结算编号
            //stkOrderStatus 合约交易状态
            //contractTimes 合约乘数
            //preSettlementPrice 昨结算
            //preOpenPosition 昨持仓量
            //exchTotalKnockQty 总成交数量
            //exchTotalKnockAmt 成交金额
            //openPosition 市场持仓量
            //closePrice 今收盘
            //settlementPrice 今结算
            //maxOrderPrice 涨停板价
            //minOrderPrice 跌停板价
            //preDelta 昨虚实度
            //delta 今虚实度
            //lastModifyTime 最后修改时间
            //mseconds 最后修改毫秒
            

        }
    }
}
