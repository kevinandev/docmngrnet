using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Bean
{

    public class MenuBean
    {
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        private bool selectable;

        public bool Selectable
        {
            get { return selectable; }
            set { selectable = value; }
        }
        private int secureLevel;

        public int SecureLevel
        {
            get { return secureLevel; }
            set { secureLevel = value; }
        }
        private int order;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        private List<MenuBean> lstSub;

        public List<MenuBean> LstSub
        {
            get { return lstSub; }
            set { lstSub = value; }
        }

    }
}