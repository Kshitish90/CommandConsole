using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandConsole
{
    public class Commands
    {
        Dictionary<string, List<string>> multiValues = null;
        public const string InfoText = "Below are the commands this console supports with examples\n" +
                        "ADD - Takes 2 arguments -> Ex: ADD foo bar\n" +
                        "KEYS - Takes 0 arguments -> Ex: KEYS\n" +
                        "MEMBERS - Takes 1 arguments -> Ex: MEMBERS foo\n" +
                        "REMOVE - Takes 2 arguments -> Ex: REMOVE foo bar\n" +
                        "REMOVEALL - Takes 1 argument -> Ex: REMOVEALL foo\n" +
                        "CLEAR- Takes 0 arguments -> Ex: CLEAR\n" +
                        "KEYEXISTS - Takes 1 argument -> Ex: KEYEXISTS foo\n" +
                        "MEMBEREXISTS - Takes 2 argument -> Ex: MEMBEREXISTS foo bar\n" +
                        "ALLMEMBERS - Takes 0 argument -> Ex: ALLMEMBERS\n" +
                        "ITEMS - Takes 0 argument -> Ex: ITEMS\n" +
                        "EXIT - Takes 0 argument -> Ex: EXIT\n" +
                        "------------------------------------------------";
        public Commands()
        {
            multiValues = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Takes the text entered in the console and executes the commands
        /// </summary>
        /// <param name="input">Text entered in the console</param>
        public string ExecuteCommand(string input)
        {
            string result = string.Empty;
            string[] words = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            switch (words[0].ToUpper())
            {
                case "ADD": result = Add(words); break;
                case "KEYS": result = ShowAllKeys(); break;
                case "MEMBERS": result = ShowMembers(words); break;
                case "REMOVE": result = Remove(words); break;
                case "REMOVEALL": result = RemoveAll(words); break;
                case "CLEAR": result = Clear(); break;
                case "KEYEXISTS": result = KeyExists(words); break;
                case "MEMBEREXISTS": result = MemberExists(words); break;
                case "ALLMEMBERS": result = AllMembers(); break;
                case "ITEMS": result = Items(); break;
                default: result = "Unable to recognise the command\n\n" + InfoText; break;
            }

            if (result.EndsWith("\n"))
                result = result.Substring(0, result.Length - 1);

            return result;
        }

        /// <summary>
        /// Adds the key and value to dictionary
        /// </summary>
        /// <param name="words">input text split by " "</param>
        private string Add(string[] words)
        {
            if (words.Length < 3)
            {
                return "ADD expects key & value pair";
            }

            string key = words[1].ToLower();
            string value = words[2];
            List<string> keyValues = multiValues.ContainsKey(key) ? multiValues[key] : new List<string>();
            if (keyValues.Contains(words[2]))
            {
                return "ERROR, member already exists for key";
            }
            else
            {
                keyValues.Add(words[2]);
                multiValues[key] = keyValues;
                return "Added";
            }
        }

        /// <summary>
        /// Displays all the keys available in the dictionary
        /// </summary>
        private string ShowAllKeys()
        {
            string result = string.Empty;
            var keys = multiValues.Keys.ToList();
            if (keys.Count == 0)
                return ") empty set";
            int counter = 1;
            keys.ForEach(x =>
            {
                result += string.Format("{0}) {1}\n", counter++, x); ;
            });

            return result;
        }

        /// <summary>
        /// Displays all the members respective to the given key
        /// </summary>
        /// <param name="words">input text split by " "</param>
        public string ShowMembers(string[] words)
        {
            if (words.Length < 2)
                return "ERROR, key name is missing";

            if (multiValues.ContainsKey(words[1].ToLower()))
            {
                string result = string.Empty;
                int counter = 1;
                multiValues[words[1].ToLower()].ForEach(x =>
                {
                    result += string.Format("{0}) {1}\n", counter++, x);
                });

                return result;
            }
            else
            {
                return "ERROR, key does not exist.";
            }
        }

        /// <summary>
        /// Removes the key and value entered from the dictionary
        /// </summary>
        /// <param name="words">input text split by " "</param>
        public string Remove(string[] words)
        {
            if (words.Length < 3)
            {
                return "ERROR, key & value are missing";
            }


            List<string> keyValues = multiValues.ContainsKey(words[1].ToLower()) ? multiValues[words[1].ToLower()] : null;
            if (keyValues == null)
            {
                return "ERROR, key does not exit";
            }
            else if (!keyValues.Contains(words[2]))
            {
                return "ERROR, member does not exit";
            }
            else
            {
                keyValues.Remove(words[2]);
                if (keyValues.Count == 0)
                    multiValues.Remove(words[1].ToLower());
                else
                    multiValues[words[1].ToLower()] = keyValues;

                return "Removed";
            }
        }

        /// <summary>
        /// Removes all the values for the given key from dictionary
        /// </summary>
        /// <param name="words">input text split by " "</param>
        private string RemoveAll(string[] words)
        {
            if (words.Length < 2)
                return "ERROR, key is missing";

            List<string> keyValues = multiValues.ContainsKey(words[1].ToLower()) ? multiValues[words[1].ToLower()] : null;
            if (keyValues == null)
                return "ERROR, key does not exit";
            else
            {
                multiValues.Remove(words[1].ToLower());
                return "Removed";
            }
        }

        /// <summary>
        /// Removes all keys and values from the dictionary
        /// </summary>
        private string Clear()
        {
            multiValues.Clear();
            return "Cleared";
        }


        /// <summary>
        /// Return true if the key exists in the dictionary, else returns false
        /// </summary>
        /// <param name="words">input text split by " "</param>
        private string KeyExists(string[] words)
        {
            if (words.Length < 2)
                return "ERROR, key is missing in command";

            return multiValues.ContainsKey(words[1].ToLower()).ToString().ToLower();
        }

        /// <summary>
        /// Returns true if the member exist against the key in the dictionary, else returns false
        /// </summary>
        /// <param name="words">input text split by " "</param>
        private string MemberExists(string[] words)
        {
            if (words.Length < 3)
                return "ERROR, key & value are missing in command";

            List<string> keyValues = multiValues.ContainsKey(words[1].ToLower()) ? multiValues[words[1].ToLower()] : null;
            if (keyValues == null)
                return "false";
            else
                return keyValues.Contains(words[2]).ToString().ToLower();
        }


        /// <summary>
        /// Displays all the members from all the keys
        /// </summary>
        private string AllMembers()
        {
            if (multiValues.Keys.Count == 0)
                return "(empty set)";

            string result = string.Empty;
            int counter = 1;
            multiValues.SelectMany(x => x.Value).ToList().ForEach(x =>
            {
                result += string.Format("{0}) {1}\n", counter++, x);
            });

            return result;
        }

        /// <summary>
        /// Displays all the keys & the values
        /// </summary>
        private string Items()
        {
            if (multiValues.Keys.Count == 0)
                return "(empty set)";

            string result = string.Empty;
            int counter = 1;
            multiValues.Keys.ToList().ForEach(x =>
            {
                multiValues[x].ForEach(y =>
                {
                    result += string.Format("{0}) {1}:{2}\n", counter++, x, y);
                });
            });

            return result;
        }

    }
}
