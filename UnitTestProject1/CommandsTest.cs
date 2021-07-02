using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandConsole;

namespace CommandConsoleTest
{
    [TestClass]
    public class CommandsTest
    {
        Commands commands = null; 
        private void init()
        {
            commands = new Commands();
            commands.ExecuteCommand("Add foo bar");
            commands.ExecuteCommand("ADD baz bang");
        }

        [TestMethod]
        public void TestAdd()
        {
            this.init();
            string result = commands.ExecuteCommand("Add foo baz");
            Assert.IsTrue(result == "Added");
        }

        [TestMethod]
        public void TestAddLessArguments()
        {
            this.init();
            string result = commands.ExecuteCommand("Add foo");
            Assert.IsTrue(result == "ADD expects key & value pair");
        }

        [TestMethod]
        public void TestAddDuplicate()
        {
            this.init();
            string result = commands.ExecuteCommand("Add foo bar");
            Assert.IsTrue(result == "ERROR, member already exists for key");
        }

        [TestMethod]
        public void TestKeys()
        {
            this.init();
            string result = commands.ExecuteCommand("keys");
            Assert.IsTrue(result == "1) foo\n2) baz");
        }

        [TestMethod]
        public void TestKeysEmptySet()
        {
            this.init();
            commands.ExecuteCommand("clear");
            string result = commands.ExecuteCommand("keys");
            Assert.IsTrue(result == ") empty set");
        }

        [TestMethod]
        public void TestMembers()
        {
            this.init();
            string result = commands.ExecuteCommand("members foo");
            Assert.IsTrue(result == "1) bar");
        }

        [TestMethod]
        public void TestMembersLessArgs()
        {
            this.init();
            string result = commands.ExecuteCommand("members");
            Assert.IsTrue(result == "ERROR, key name is missing");
        }

        [TestMethod]
        public void TestMembersKeyDoesNotExist()
        {
            this.init();
            string result = commands.ExecuteCommand("members foo1");
            Assert.IsTrue(result == "ERROR, key does not exist.");
        }

        [TestMethod]
        public void TestRemove()
        {
            this.init();
            string result = commands.ExecuteCommand("remove foo bar");
            Assert.IsTrue(result == "Removed");
        }

        [TestMethod]
        public void TestRemoveLessArgs()
        {
            this.init();
            string result = commands.ExecuteCommand("remove foo");
            Assert.IsTrue(result == "ERROR, key & value are missing");
        }

        [TestMethod]
        public void TestRemoveDoesNotExist()
        {
            this.init();
            string result = commands.ExecuteCommand("remove foo1 bar1");
            Assert.IsTrue(result == "ERROR, key does not exit");
        }

        [TestMethod]
        public void TestRemoveMemberDoesNotExist()
        {
            this.init();
            string result = commands.ExecuteCommand("remove foo bar1");
            Assert.IsTrue(result == "ERROR, member does not exit");
        }

        [TestMethod]
        public void TestRemoveAll()
        {
            this.init();
            string result = commands.ExecuteCommand("removeall foo");
            Assert.IsTrue(result == "Removed");
        }

        [TestMethod]
        public void TestRemoveAllMissingKey()
        {
            this.init();
            string result = commands.ExecuteCommand("removeall");
            Assert.IsTrue(result == "ERROR, key is missing");
        }

        [TestMethod]
        public void TestRemoveAllKeyDoesNotExist()
        {
            this.init();
            string result = commands.ExecuteCommand("removeall foo1");
            Assert.IsTrue(result == "ERROR, key does not exit");
        }

        [TestMethod]
        public void TestClear()
        {
            this.init();
            string result = commands.ExecuteCommand("CLEAR");
            Assert.IsTrue(result == "Cleared");
        }

        [TestMethod]
        public void TestKeyExists()
        {
            this.init();
            string result = commands.ExecuteCommand("keyexists foo");
            Assert.IsTrue(result == "true");
        }

        [TestMethod]
        public void TestKeyExistsMissingKey()
        {
            this.init();
            string result = commands.ExecuteCommand("keyexists");
            Assert.IsTrue(result == "ERROR, key is missing in command");
        }

        [TestMethod]
        public void TestMemberExists()
        {
            this.init();
            string result = commands.ExecuteCommand("memberexists foo bar");
            Assert.IsTrue(result == "true");
        }

        [TestMethod]
        public void TestMemberExistsKeyValueMissing()
        {
            this.init();
            string result = commands.ExecuteCommand("memberexists foo");
            Assert.IsTrue(result == "ERROR, key & value are missing in command");
        }

        [TestMethod]
        public void TestAllMembers()
        {
            this.init();
            string result = commands.ExecuteCommand("ALLMEMBERS");
            Assert.IsTrue(result == "1) bar\n2) bang");
        }

        [TestMethod]
        public void TestAllMembersEmptySet()
        {
            this.init();
            commands.ExecuteCommand("clear");
            string result = commands.ExecuteCommand("ALLMEMBERS");
            Assert.IsTrue(result == "(empty set)");
        }

        [TestMethod]
        public void TestItems()
        {
            this.init();
            string result = commands.ExecuteCommand("items");
            Assert.IsTrue(result == "1) foo:bar\n2) baz:bang");
        }

        [TestMethod]
        public void TestItemsEmptySet()
        {
            this.init();
            commands.ExecuteCommand("clear");
            string result = commands.ExecuteCommand("items");
            Assert.IsTrue(result == "(empty set)");
        }
    }
}
