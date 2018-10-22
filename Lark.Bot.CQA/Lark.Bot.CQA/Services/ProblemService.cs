using Lark.Bot.CQA.Uitls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lark.Bot.CQA.Services.CoinmarketcapService;

namespace Lark.Bot.CQA.Services
{
    public class ProblemService : IProblemService
    {
        public const string api = "http://problem.evesgf.com/api/Problem";

        public string GetRundomProblem()
        {

            var reStr = HttpUitls.Get(api);
            var model = JsonConvert.DeserializeObject<QuestionModel>(reStr);

            return "[提问]"+model.Title+"\n"+"[说明]"+model.Detail;
        }

        public string GetRundomRoblemToTag(string key)
        {
            var reStr = HttpUitls.Get(api+"/"+key);
            var model = JsonConvert.DeserializeObject<QuestionModel>(reStr);

            return "[提问]" + model.Title + "\n" + "[说明]" + model.Detail;
        }
    }

    public class QuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Detail { get; set; }

        public virtual ICollection<AnswerModel> Answers { get; set; }

        public string AddTime { get; set; }
    }

    public class AnswerModel
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public virtual QuestionModel Quesiton { get; set; }
        public string Content { get; set; }

        public string AddTime { get; set; }
    }
}
