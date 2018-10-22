using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public interface IProblemService
    {
        string GetRundomProblem();
        string GetRundomRoblemToTag(string key);
    }
}
