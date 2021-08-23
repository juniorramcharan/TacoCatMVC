using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TacoCatMVC.Models;

namespace TacoCatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Reverse()
        {
            Palindrome model = new Palindrome();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reverse(Palindrome palindrome)
        {
            string inputWord = palindrome.InputWord;
            string reverseWord = "";

            for(int i = inputWord.Length-1; i>=0; i--)
            {
                reverseWord += inputWord[i];
            }
            palindrome.RevWord = reverseWord;

            reverseWord = Regex.Replace(reverseWord.ToLower(),"[^a-zA-Z0-9]+","");
            inputWord = Regex.Replace(reverseWord.ToLower(), "[^a-zA-Z0-9]+", "");

            if(reverseWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $" Success {palindrome.InputWord} is a Palindrome";
            }
            else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $" Success {palindrome.InputWord} is not a Palindrome";
            }
            return View(palindrome);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
