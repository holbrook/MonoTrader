using System;
namespace Tangle.Trading.RootNetAdapter
{
    public class RNField
    {
        public int Flag { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

        public RNField()
        {

        }

        public RNField(int flag, string fieldName, string fieldValue)
        {
            Flag = flag;
            FieldName = fieldName;
            FieldValue = fieldValue;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}={2}", Flag, FieldName, FieldValue);
        }
    }
}
