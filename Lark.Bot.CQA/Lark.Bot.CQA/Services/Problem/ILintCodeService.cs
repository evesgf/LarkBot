using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services.Problem
{
    public interface ILintCodeService
    {
        string GetNowProblem();
        string RefshNowProblem();
        string GetNowAnswer();
    }
}
