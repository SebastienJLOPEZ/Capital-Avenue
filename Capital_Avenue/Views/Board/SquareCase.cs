﻿using Capital_Avenue.Models;
using Capital_Avenue.Properties;
using Capital_Avenue.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Capital_Avenue.Views.Board
{
    public partial class SquareCase : Case
    {
        FlowLayoutPanel flow;
        public SquareCase() : base()
        {
            this.Size = new Size(122, 122);
            this.BorderStyle = BorderStyle.FixedSingle;

            XmlDocument MyXmlFile = new XmlDocument();
            //parcouris les elements du fichiers
            //MyXmlFile.LoadXml("C:/capital - avenue/Capital_Avenue/Resources/BoardXML");
            //XmlNode xmlValueItem = MyXmlFile.GetElementsByTagName("terrain")[37];
            //string position = xmlValueItem.Attributes["id"].InnerText;
        }
    }
}
