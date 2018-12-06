using Lark.Bot.CQA.Uitls;
using Lark.Bot.CQA.Uitls.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services.Problem
{
    public class LintCodeService : ILintCodeService
    {
        public string GetNowAnswer()
        {
            return "请赞美杨老师，说：杨老师好帅";
        }

        public string GetNowProblem()
        {
            if (ConfigManager.Instance().problem==null || string.IsNullOrEmpty((ConfigManager.Instance().problem.unique_name)))
            {
                return RefshNowProblem();
            }

            var msg = ConfigManager.Instance().problem.description + "\n" + ConfigManager.Instance().problem.example;
            return msg;
        }

        public string RefshNowProblem()
        {
            string url = "https://www.lintcode.com/api/problems/?page=";
            var a = JsonConvert.DeserializeObject<ProblemList>(HttpUitls.Get(url + "1"));
            flag: var pageCount = new Random().Next(0, a.maximum_page);
            var b = JsonConvert.DeserializeObject<ProblemList>(HttpUitls.Get(url + pageCount.ToString()));
            List<Problem> l = new List<Problem>();
            foreach (var p in b.problems)
            {
                if (p.status == 1) l.Add(p);
            }
            if (l.Count == 0) goto flag;

            var problemCount = new Random().Next(0, l.Count());
            var problem = b.problems[problemCount];

            string url2 = "https://www.lintcode.com/api/problems/detail/?unique_name_or_alias=" + problem.unique_name + "&_format=detail";
            var d = JsonConvert.DeserializeObject<ProblemDetial>(HttpUitls.Get(url2));

            ConfigManager.Instance().problem = d;
            ConfigManager.Instance().refshProblemDateTime = DateTime.Now;
            ConfigManager.Instance().pushProblemCount = 0;

            return d.description+ "\n" + d.example;
        }
    }


    public class ProblemList
    {
        public int count { get; set; }
        public int maximum_page { get; set; }
        public string ordering { get; set; }
        public int current_page { get; set; }
        public Problem[] problems { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
    }

    public class Problem
    {
        public int id { get; set; }
        public string title { get; set; }
        public string unique_name { get; set; }
        public int accepted_rate { get; set; }
        public bool is_new { get; set; }
        public int level { get; set; }
        public int status { get; set; }
        public int frequency { get; set; }
        public string[] company_tags { get; set; }
        public int comment_count { get; set; }
        public bool is_unlocked { get; set; }
    }


    public class ProblemDetial
    {
        public int id { get; set; }
        public string unique_name { get; set; }
        public object user_status { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public bool is_favorited { get; set; }
        public bool generatable { get; set; }
        public string version { get; set; }
        public int level { get; set; }
        public int accepted_rate { get; set; }
        public string description { get; set; }
        public string notice { get; set; }
        public string clarification { get; set; }
        public string example { get; set; }
        public string challenge { get; set; }
        public Tag[] tags { get; set; }
        public object[] related_problems { get; set; }
        public int comment_count { get; set; }
        public string testcase_sample { get; set; }
        public int total_accepted { get; set; }
        public int total_submissions { get; set; }
        public string contest { get; set; }
        public bool has_followed_by { get; set; }
        public string[] accept_languages { get; set; }
        public bool in_green_channel { get; set; }
        public bool is_highlighted { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cn_name { get; set; }
        public int type { get; set; }
        public string unique_name { get; set; }
        public string alias { get; set; }
        public int problem_count { get; set; }
    }


}
