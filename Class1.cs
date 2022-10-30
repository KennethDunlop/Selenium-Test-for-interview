using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V104.Network;
using OpenQA.Selenium.DevTools.V104.Page;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace Test
{
    public class Class1
    {
        ChromeDriver driver = new ChromeDriver("C:\\Users\\interviewee\\Desktop\\C#-selenium\\chromedriver");
        [Fact]
        public void FirstTest() {
            /*
            1.	Add two todo items and assert that they are displaying in the list
            2.	Tick a todo item and assert that it displays on the complete tab
            3.	Create a todo item, remove the todo item, assert that it is no longer in the list
            */

            // Also here: https://todomvc.com/examples/angular2/
            // A JavaScript version with the tabs is here: https://todomvc.com/examples/angularjs/#/
            driver.Navigate().GoToUrl("https://todomvc.com/examples/angular2/");//Navigate to the todolist webpage.
            //Part 1: Add two todo items and assert that they are displaying in the list
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var inputBox = wait.Until(e => e.FindElement(By.ClassName("new-todo")));//This 'wait.Until' makes the test more reliable.
            //inputBox.SendKeys("Task 1");
            inputBox.SendKeys("T");//Doing each keystroke individually here seems to make the test more reliable.
            inputBox.SendKeys("a");
            inputBox.SendKeys("s");
            inputBox.SendKeys("k");
            inputBox.SendKeys(" ");
            inputBox.SendKeys("1");
            inputBox.SendKeys(Keys.Return);
            inputBox.SendKeys("T");
            inputBox.SendKeys("a");
            inputBox.SendKeys("s");
            inputBox.SendKeys("k");
            inputBox.SendKeys(" ");
            inputBox.SendKeys("2");
            inputBox.SendKeys(Keys.Return);
            //inputBox.SendKeys("Task 3");//Enable these two lines to get the test to fail, with 3 Tasks instead of 2.
            //inputBox.SendKeys(Keys.Return);
            var query = driver.FindElements(By.TagName("li"));//Find every list item (hopefully 2).
            foreach (var element in query)
                {
                Assert.Contains("Task", element.Text);//Ensure that each task contains 'Task' in its name.
                }
            Assert.Equal(2, query.Count);//Test that there are two items in the to-do list.
            //driver.Quit(); //Close the browser window when done with the first page.

            //Part 2: Tick a todo item and assert that it displays on the complete tab
            driver.Navigate().GoToUrl("https://todomvc.com/examples/angularjs/#/");//Navigate to the todolist webpage that has tabs.
            inputBox = wait.Until(e => e.FindElement(By.ClassName("new-todo")));//This 'wait.Until' makes the test more reliable.
            inputBox.SendKeys("Task 1");
            inputBox.SendKeys(Keys.Return);
            inputBox.SendKeys("Task 2");
            inputBox.SendKeys(Keys.Return);
            var togglebutton = driver.FindElement(By.ClassName("toggle"));//Find the first toggle button
            togglebutton.Click();//Click the first toggle button. It should get ticked.
            var filters = driver.FindElements(By.ClassName("filters"));//Find the filter buttons.
            var completedbutton = driver.FindElement(By.LinkText("Completed"));
            completedbutton.Click();
            var query2 = driver.FindElements(By.TagName("li"));//Find every list item (hopefully 1 this time).
            foreach (var item in query2)
                {
                if (item.Text == "Completed")
                    {
                    item.Click();//Click the 'Completed' button to go to the 'Completed' tab.
                    }
                }
            var task2 = driver.FindElements(By.ClassName("destroy"));//Find the number of task-deleting buttons.
            Assert.Equal(1, task2.Count);//Check that there is only one task in the 'Completed' list.
            var parent = driver.FindElement(By.ClassName("todo-list"));//Get the parent todolist.
            var child = parent.FindElement(By.TagName("label"));//Find the label inside the parent todolist.
            Assert.Equal("Task 1", child.Text);//Check that the task is labelled 'Task 1'.

            //Part 3: Create a todo item, remove the todo item, assert that it is no longer in the list
            //Since an item is already in the list, I can clear completed items and check the length of that list.
            var buttons = driver.FindElement(By.ClassName("view"));
            //var deletebutton = buttons.FindElement(By.ClassName("destroy"));
            var deletebutton = driver.FindElement(By.ClassName("clear-completed"));
            deletebutton.Click();//Click 'Clear completed' to remove the item from the list.
            //var parent2 = driver.FindElement(By.ClassName("todo-list"));//Get the parent todolist again.
            var child2 = parent.FindElements(By.TagName("li"));
            Assert.Equal(0, child2.Count);//Test that there are no items in the to-do list any more.
            driver.Quit(); //Close the browser window when done.
        }
    }
}
