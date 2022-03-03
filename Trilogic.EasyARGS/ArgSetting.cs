using System;
using System.Collections.Generic;
using System.Text;

namespace Trilogic.EasyARGS
{
    public class ArgSetting
    {
        #region Class Members
        protected string key;
        protected string val;
        #endregion

        #region Constructors and Destructors
        internal ArgSetting()
        {
        }
        internal ArgSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }
        internal ArgSetting(string[] arg)
        {
            if (arg == null || arg.Length < 1)
                throw new ArgException("invalid arg array");

            Key = arg[0];

            if (string.IsNullOrEmpty(key))
                throw new ArgException("invalid arg array");

            if (arg.Length > 1)
                Value = arg[1];
        }
        #endregion

        #region Class Properties
        public string Key
        {
            get { return key; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    key = value.Trim();
                if (string.IsNullOrEmpty(key))
                    throw new ArgException("invalid setting");
            }
        }

        public string Value
        {
            get { return val; }
            set { val = value; }
        }
        #endregion
    }

}
