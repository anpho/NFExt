using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using dll_newFolderEx;
using System.Collections;

namespace nfext_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_findTheSame()
        {
            ArrayList pathlist = new ArrayList();
            pathlist.Add("新建文件夹（1）");
            pathlist.Add("新建文件夹（2）");
            pathlist.Add("新建文件夹");
            NewFolderForm nf = new NewFolderForm(pathlist);

            Assert.AreEqual("新建文件夹", nf.Find_the_same());

            nf.Dispose();
            pathlist.Clear();
        }
    }
}
