using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownSharp;

namespace BaoVietCore.Services
{
    public class MarkDownService : ServiceBase, IMarkDownParser
    {
        Markdown Markdown;

        public MarkDownService(Manager man) : base(man)
        {
            Markdown = new Markdown();
            Markdown.AutoHyperlink = true;
            Markdown.AutoNewLines = true;
        }

        public string ConvertToMarkDown(string source)
        {
            return Markdown.Transform(source);
        }
    }
}
