using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project2Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileType;
            Console.WriteLine("This program will show the SuperBowl stats in a preferred viewing method (HTLM or TXT)");

            Console.WriteLine("Please enter your preferred method - TXT or HTML");
            fileType = Console.ReadLine();
            fileType = fileType.ToUpper();

            if (fileType == "TXT")
            {
                List<Stats> listofStats = new List<Stats>();
                listofStats = ReadFile();
                WriteFile(listofStats);
            }
            else if (fileType == "HTML") //FINISH THIS WITH NEW METHOD (CreateHTML():)
            {
                List<Stats> listofStats = new List<Stats>();
                listofStats = ReadFile();
                WriteFile(listofStats);
            }
            else
            {
                Console.WriteLine("Invalid Entry. Please try again.");
                Main(args);
            }
        }

        public static List<Stats> ReadFile()
        {
            List<Stats> statsInfo = new List<Stats>();

            NFLList NFLStats;
            List<Double> attendance = new List<Double>();

            string[] statsValues;
            string NFLFile = @"C:\Users\olubeno\OneDrive - dunwoody.edu\Spring 2018\CWEB - Advanced Programming\Visual Studio\Project 2\Super_Bowl_Project.csv";

            try
            {
                FileStream file = new FileStream(NFLFile, FileMode.Open, FileAccess.Read);
                StreamReader readIn = new StreamReader(file);

                string header = readIn.ReadLine();

                while (!readIn.EndOfStream)
                {
                    statsValues = readIn.ReadLine().Split(',');

                    NFLStats = new NFLList(statsValues[0], statsValues[1], Convert.ToInt32(statsValues[2]), statsValues[3], statsValues[4], statsValues[5], Convert.ToInt32(statsValues[6]), statsValues[7], statsValues[8], statsValues[9], Convert.ToInt32(statsValues[10]), statsValues[11], statsValues[12], statsValues[13], statsValues[14]);
                    //writeText.WriteLine(winStats);

                    string totalAttendance = statsValues[2];
                    statsInfo.Add(NFLStats);
                    attendance.Add(Convert.ToDouble(totalAttendance));
                }

                
                readIn.Close();
                file.Close();
                double average = attendance.Sum();
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Console.WriteLine("The program will now end");
                Console.ReadLine();
            }

            return statsInfo;

        }

        public static void WriteFile(List<Stats> statsInfo)
        {
            /*WinnersList winStats;
            FiveList fiveStats;
            MostList mostStats;
            MVPList mvpStats;*/

            //CreateTheFile
            string FilePath;
            string FileName;
            Console.WriteLine("Please type the name of the file. (You don't need to type the extention)");
            FileName = Console.ReadLine();
            Console.WriteLine("Where would you like to save the file to?");
            FilePath = Console.ReadLine() + @"\" + FileName + ".txt"; //The Full name of txt file
            Console.WriteLine("Here is your full file name:");
            Console.WriteLine(FilePath);
            Console.ReadLine();

            TextWriter writeText = new StreamWriter(FilePath);

            writeText.WriteLine();

            writeText.WriteLine("1. Which coach lost the most super bowls? \n/ Bud Grant, Dan Reeves, Don Shula and Marv Levy");
            writeText.WriteLine();
            writeText.WriteLine("2. Which coach won the most super bowls? \n/ Bill Belichick ");
            writeText.WriteLine();
            writeText.WriteLine("3. Which team(s) won the most super bowls? \n/ Pittsburg Steelers");
            writeText.WriteLine();
            writeText.WriteLine("4. Which team(s) lost the most super bowls? \n/ Denver Broncos");
            writeText.WriteLine();
            writeText.WriteLine("5. Which SuperBowl had the greatest point difference? \n/ SuperBowl XXIV");
            writeText.WriteLine();
            writeText.WriteLine("6.What is the average attendance of all super bowls? \n/ 77,988 people");

            writeText.WriteLine();

            //Winners of SB
            var Winners =
            from games in statsInfo
            select new { games.WinnerFromFile, games.DateFromFile, games.QBWinFromFile, games.CoachWinFromFile, games.MVPFromFile, games.WinPtFromFile, games.LosingPtFromFile };

            writeText.WriteLine("SuperBowl Winners:");
            
            foreach (var games in statsInfo)
            {
                writeText.WriteLine($"Team: {games.WinnerFromFile } \n Year: {games.DateFromFile } \n QB: {games.QBWinFromFile } \n Coach: {games.CoachWinFromFile } \n MVP: {games.MVPFromFile } \n Point Difference: {games.WinPtFromFile - games.LosingPtFromFile }");
                writeText.WriteLine();
            }
            writeText.WriteLine();
            
            //Attendance of SB
            var AttendSB =
            from Attend in statsInfo
            where Attend.AttFromFile >= 100000  
            select new { Attend.DateFromFile, Attend.WinnerFromFile, Attend.LoserFromFile, Attend.CityFromFile, Attend.StateFromFile, Attend.StadiumFromFile };

            writeText.WriteLine("Top 5 attended SuperBowls:");
            foreach (var Attend in AttendSB)
            {
                writeText.WriteLine($"Year: {Attend.DateFromFile} \n Winning Team: {Attend.WinnerFromFile} \n Losing Team: {Attend.LoserFromFile } \n City: {Attend.CityFromFile } State: {Attend.StateFromFile } \n Stadium: {Attend.StadiumFromFile} ");
            }
            writeText.WriteLine();

            //Most States of SB
            var stateList =
            from states in statsInfo
            select new { states.CityFromFile, states.StateFromFile, states.StadiumFromFile };

            writeText.WriteLine("States that hosted the most SuperBowls:");

            foreach (var states in stateList)
            {
                if (stateList.Distinct().Count() >= 14)
                {
                    writeText.WriteLine($"City: {states.CityFromFile } \n State: {states.StateFromFile } \n Stadium: {states.StadiumFromFile }");

                }
            }
            writeText.WriteLine();

            //MVPs of SB
            var MVPList =
            from mvp in statsInfo
            select new { mvp.MVPFromFile, mvp.WinnerFromFile, mvp.LoserFromFile };

            writeText.WriteLine("Players who won MVP more than 2 times:");
            foreach (var mvp in MVPList)
            {
                if (stateList.Distinct().Count() >= 5)
                {
                    writeText.WriteLine($"MVP: {mvp.MVPFromFile } \n Winning Team: {mvp.WinnerFromFile } \n Losing Team: {mvp.LoserFromFile }");
                }
                else
                {
                    writeText.WriteLine("No one is that good.");
                }
            }


            
        }

 

      /*  public static void WriteFile(List<Stats> statsInfo)
        {
            try
            {
                WinnersList winStats;
                FiveList fiveStats;
                MostList mostStats;
                MVPList mvpStats;

                string FilePath;
                string FileName;

                Console.WriteLine("Please type the name of the file. (You don't need to type the extention)");
                FileName = Console.ReadLine();
                Console.WriteLine("Where would you like to save the file to?");
                FilePath = Console.ReadLine() + @"\" + FileName + ".txt"; //The Full name of txt file
                Console.WriteLine("Here is your full file name:");
                Console.WriteLine(FilePath);
                Console.ReadLine();

                TextWriter writeText = new StreamWriter(FilePath);



                //writeText.WriteLine("List of SuperBowl Winners:");
                foreach (var row in statsInfo)
                {
                    writeText.WriteLine(row);
                }
                writeText.Close();

              
            }

            catch (Exception i)
            {
                Console.WriteLine("The file could not be created.");
                Console.WriteLine(i.Message);
                Console.WriteLine("The program will now close");
                Console.ReadLine();
            }
        }*/
    }


    //Starting Other classes
    public abstract class Stats
    {
        public string DateFromFile { get; set; }
        public string SBFromFile { get; set; }
        public double AttFromFile { get; set; }
        public string QBWinFromFile { get; set; }
        public string CoachWinFromFile { get; set; }
        public string WinnerFromFile { get; set; }
        public int WinPtFromFile { get; set; }
        public string QBLoserFromFile { get; set; }
        public string CoachLostFromFile { get; set; }
        public string LoserFromFile { get; set; }
        public int LosingPtFromFile { get; set; }
        public string MVPFromFile { get; set; }
        public string StadiumFromFile { get; set; }
        public string CityFromFile { get; set; }
        public string StateFromFile { get; set; }

        public Stats(string DateFromFile, string SBFromFile, double AttFromFile,
            string QBWinFromFile, string CoachWinFromFile, string WinnerFromFile,
            int WinPtFromFile, string QBLoserFromFile, string CoachLostFromFile,
            string LoserFromFile, int LosingPtFromFile, string MVPFromFile,
            string StadiumFromFile, string CityFromFile, string StateFromFile)
        {
            this.DateFromFile = DateFromFile;
            this.SBFromFile = SBFromFile;
            this.AttFromFile = AttFromFile;
            this.QBWinFromFile = QBWinFromFile;
            this.CoachWinFromFile = CoachWinFromFile;
            this.WinnerFromFile = WinnerFromFile;
            this.WinPtFromFile = WinPtFromFile;
            this.QBLoserFromFile = QBLoserFromFile;
            this.CoachLostFromFile = CoachLostFromFile;
            this.LoserFromFile = LoserFromFile;
            this.LosingPtFromFile = LosingPtFromFile;
            this.MVPFromFile = MVPFromFile;
            this.StadiumFromFile = StadiumFromFile;
            this.CityFromFile = CityFromFile;
            this.StateFromFile = StateFromFile;

        }


    }
    //Second class for NFL List itself
    class NFLList : Stats
    {
        public NFLList(string DateFromFile, string SBFromFile, double AttFromFile,
            string QBWinFromFile, string CoachWinFromFile, string WinnerFromFile,
            int WinPtFromFile, string QBLoserFromFile, string CoachLostFromFile,
            string LoserFromFile, int LosingPtFromFile, string MVPFromFile,
            string StadiumFromFile, string CityFromFile, string StateFromFile) : base(DateFromFile, SBFromFile, AttFromFile, QBWinFromFile, CoachWinFromFile, WinnerFromFile, WinPtFromFile, QBLoserFromFile, CoachLostFromFile, LoserFromFile, LosingPtFromFile,MVPFromFile, StadiumFromFile, CityFromFile, StateFromFile)
        {
            this.DateFromFile = DateFromFile;
            this.SBFromFile = SBFromFile;
            this.AttFromFile = AttFromFile;
            this.QBWinFromFile = QBWinFromFile;
            this.CoachWinFromFile = CoachWinFromFile;
            this.WinnerFromFile = WinnerFromFile;
            this.WinPtFromFile = WinPtFromFile;
            this.QBLoserFromFile = QBLoserFromFile;
            this.CoachLostFromFile = CoachLostFromFile;
            this.LoserFromFile = LoserFromFile;
            this.LosingPtFromFile = LosingPtFromFile;
            this.MVPFromFile = MVPFromFile;
            this.StadiumFromFile = StadiumFromFile;
            this.CityFromFile = CityFromFile;
            this.StateFromFile = StateFromFile;
        }
    }

   /* class FiveList : Stats
    {
        public FiveList(string DateFromFile, string SBFromFile, double AttFromFile,
            string QBWinFromFile, string CoachWinFromFile, string WinnerFromFile,
            int WinPtFromFile, string QBLoserFromFile, string CoachLostFromFile,
            string LoserFromFile, int LosingPtFromFile, string MVPFromFile,
            string StadiumFromFile, string CityFromFile, string StateFromFile) : base(DateFromFile, SBFromFile, AttFromFile, QBWinFromFile, CoachWinFromFile, WinnerFromFile, WinPtFromFile, QBLoserFromFile, CoachLostFromFile, LoserFromFile, LosingPtFromFile,MVPFromFile, StadiumFromFile, CityFromFile, StateFromFile)
        {
            /*this.DateFromFile = DateFromFile;
            this.SBFromFile = SBFromFile;
            this.AttFromFile = AttFromFile;
            this.QBWinFromFile = QBWinFromFile;
            this.CoachWinFromFile = CoachWinFromFile;
            this.WinnerFromFile = WinnerFromFile;
            this.WinPtFromFile = WinPtFromFile;
            this.QBLoserFromFile = QBLoserFromFile;
            this.CoachLostFromFile = CoachLostFromFile;
            this.LoserFromFile = LoserFromFile;
            this.LosingPtFromFile = LosingPtFromFile;
            this.MVPFromFile = MVPFromFile;
            this.StadiumFromFile = StadiumFromFile;
            this.CityFromFile = CityFromFile;
            this.StateFromFile = StateFromFile;
        }

        public override string ToString()
        {
            return String.Format($"Year: {DateFromFile} \n Winning Team Name: {WinnerFromFile } \n Loing Team: {LoserFromFile } \n City: {CityFromFile } \n State: {StateFromFile } \n Stadium: {StadiumFromFile } ");

        }
    }

    class MostList : Stats
    {
        public MostList(string DateFromFile, string SBFromFile, double AttFromFile,
            string QBWinFromFile, string CoachWinFromFile, string WinnerFromFile,
            int WinPtFromFile, string QBLoserFromFile, string CoachLostFromFile,
            string LoserFromFile, int LosingPtFromFile, string MVPFromFile,
            string StadiumFromFile, string CityFromFile, string StateFromFile) : base(DateFromFile, SBFromFile, AttFromFile, QBWinFromFile, CoachWinFromFile, WinnerFromFile, WinPtFromFile, QBLoserFromFile, CoachLostFromFile, LoserFromFile, LosingPtFromFile, MVPFromFile, StadiumFromFile, CityFromFile, StateFromFile)
        {
            /*this.DateFromFile = DateFromFile;
            this.SBFromFile = SBFromFile;
            this.AttFromFile = AttFromFile;
            this.QBWinFromFile = QBWinFromFile;
            this.CoachWinFromFile = CoachWinFromFile;
            this.WinnerFromFile = WinnerFromFile;
            this.WinPtFromFile = WinPtFromFile;
            this.QBLoserFromFile = QBLoserFromFile;
            this.CoachLostFromFile = CoachLostFromFile;
            this.LoserFromFile = LoserFromFile;
            this.LosingPtFromFile = LosingPtFromFile;
            this.MVPFromFile = MVPFromFile;
            this.StadiumFromFile = StadiumFromFile;
            this.CityFromFile = CityFromFile;
            this.StateFromFile = StateFromFile;
        }

        public override string ToString()
        {
            return String.Format($" City: {CityFromFile } \n State: {StateFromFile } \n Stadium: {StadiumFromFile } ");

        }
    }

    class MVPList : Stats
    {
        public MVPList(string DateFromFile, string SBFromFile, double AttFromFile,
            string QBWinFromFile, string CoachWinFromFile, string WinnerFromFile,
            int WinPtFromFile, string QBLoserFromFile, string CoachLostFromFile,
            string LoserFromFile, int LosingPtFromFile, string MVPFromFile,
            string StadiumFromFile, string CityFromFile, string StateFromFile) : base(DateFromFile, SBFromFile, AttFromFile, QBWinFromFile, CoachWinFromFile, WinnerFromFile, WinPtFromFile, QBLoserFromFile, CoachLostFromFile, LoserFromFile, LosingPtFromFile, MVPFromFile, StadiumFromFile, CityFromFile, StateFromFile)
        { 
           /* this.WinnerFromFile = WinnerFromFile;
            this.LoserFromFile = LoserFromFile;
            this.MVPFromFile = MVPFromFile;
            this.StadiumFromFile = StadiumFromFile;
            this.QBLoserFromFile = QBLoserFromFile;
            this.CoachLostFromFile = CoachLostFromFile;
            this.WinPtFromFile = WinPtFromFile;
            this.LosingPtFromFile = LosingPtFromFile;
            this.CityFromFile = CityFromFile;
            this.DateFromFile = DateFromFile;
            this.SBFromFile = SBFromFile;
            this.AttFromFile = AttFromFile;
            this.QBWinFromFile = QBWinFromFile;
            this.CoachWinFromFile = CoachWinFromFile;
            this.StateFromFile = StateFromFile;
        }

        public override string ToString()
        {
            return String.Format($"MVP: {MVPFromFile } \n Winning Team: {WinnerFromFile } \n Losing Team: {LoserFromFile } ");

        }
    }*/

}
