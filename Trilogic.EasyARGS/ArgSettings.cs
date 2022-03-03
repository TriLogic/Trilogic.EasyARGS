using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trilogic.EasyARGS
{
    public class ArgSettings
    {
        #region Static Members
        private static char[] argSplitter = { '=', ':' };
        #endregion

        #region Class Members
        protected Dictionary<String, ArgSetting> store;
        #endregion

        #region Constructors and Destructors
        public ArgSettings()
        {
            store = new Dictionary<string, ArgSetting>();
        }

        public ArgSettings(Dictionary<string,ArgSetting> storage)
        {
            store = storage;
        }

        public ArgSettings(string[] args)
        {
            store = new Dictionary<string, ArgSetting>();
            ParseArgs(args);
        }
        #endregion

        #region Class Properties
        public int Count
        {
            get { return store.Count; }
        }

        public ArgSetting this[string key]
        {
            get { return store[KeyOf(key)]; }
            set { store[KeyOf(key)] = value; }
        }

        public void Clear()
        {
            store.Clear();
        }

        public Dictionary<string, ArgSetting> Storage
        {
            get { return store; }
        }
        #endregion

        #region Key Generation
        public string KeyOf(ArgSetting value)
        {
            return KeyOf(value.Key);
        }
        public string KeyOf(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("invalid key");
            return value.ToLower();
        }
        #endregion

        #region Exists, Set, Set, Remove Methods
        public bool Exists(string key)
        {
            return store.ContainsKey(KeyOf(key));
        }

        public bool Exists(string keyA, string keyB)
        {
            return store.ContainsKey(KeyOf(keyA)) ||
                store.ContainsKey(KeyOf(keyB));
        }

        public ArgSetting Set(ArgSetting setting)
        {
            string key = KeyOf(setting);
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("invalid key");
            if (store.ContainsKey(key))
                store.Remove(key);
            store[key] = setting;
            return setting;
        }
        public ArgSetting Set(string key, string value)
        {
            return Set(new ArgSetting(key, value));
        }

        public ArgSetting Get(string key, string defValue)
        {
            ArgSetting s = Get(key);
            if (s != null)
                return s;
            return Set(new ArgSetting(key, defValue));
        }

        public ArgSetting Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("invalid key");
            string lowkey = KeyOf(key);
            if (store.ContainsKey(lowkey))
                return store[lowkey];
            return null;
        }

        public ArgSetting Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("invalid key");
            string lowkey = KeyOf(key);
            if (store.ContainsKey(lowkey))
            {
                ArgSetting value = store[lowkey];
                store.Remove(lowkey);
                return value;
            }
            return null;
        }
        #endregion

        #region Argument Parsing
        public static ArgSettings ParseSettings(string[] args)
        {
            ArgSettings s = new ArgSettings();
            s.ParseArgs(args);
            return s;
        }

        public void ParseArgs(string[] args)
        {
            string[] arg = new string[2];

            if (store.Count > 0)
                store.Clear();

            if (args == null || args.Length < 1)
                return;

            for (int i = 0; i < args.Length; i++)
            {
                arg[0] = string.Empty;
                arg[1] = string.Empty;

                // locate the first split char (if any)
                int idx = args[i].IndexOfAny(argSplitter);

                if (idx < 0)
                {
                    arg[0] = args[i];
                }
                else if (idx == 0)
                {
                    throw new ArgException("invalid key");
                }
                else
                {
                    arg[0] = args[i].Substring(0, idx).TrimEnd();

                    if (idx + 1 < args[0].Length)
                        arg[1] = args[i].Substring(idx + 1).Trim();
                }

                if (arg[0].StartsWith("/") || arg[0].StartsWith("-"))
                {
                    if (arg[0].Length < 2)
                        throw new ArgException("invalid argument");
                    arg[0] = arg[0].Substring(1).TrimEnd();
                }

                Set(new ArgSetting(arg));
            }
        }
        #endregion

        #region Assert Operations
        public void Assert(string key, string msg)
        {
            if (!Exists(key))
                throw new ArgException(msg);
        }
        public void AssertAnd(string keyA, string keyB, string msg)
        {
            Assert(keyA, msg);
            Assert(keyB, msg);
        }

        public void AssertAnd(IEnumerable<string> keys, string msg)
        {
            foreach (string key in keys)
                Assert(key, msg);
        }

        public void AssertOr(string keyA, string keyB, string msg)
        {
            if (Exists(keyA) || Exists(keyB))
                return;
            throw new ArgException(msg);
        }

        public void AssertOr(IEnumerable<string> keys, string msg)
        {
            foreach (string key in keys)
                Assert(key, msg);
        }

        public void AssertXor(string keyA, string keyB, string msg)
        {
            if (Exists(keyA) ^ Exists(keyB))
                return;
            throw new ArgException(msg);
        }
        public void AssertXor(IEnumerable<string> keys, string msg)
        {
            int found = 0;
            foreach (string key in keys)
            {
                found += Exists(key) ? 1 : 0;
                if (found > 1)
                    throw new ArgException(msg);
            }
            if (found == 0)
                throw new ArgException(msg);
        }

        #endregion
    }
}
