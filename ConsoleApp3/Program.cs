using System;
using System.IO;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            // input 을 가져온다
            var input = File.ReadAllText("./input.txt");

            // 명령어를 라인 단위로 나눔
            var cmds = input.Split("\n");

            // 대기장소의 수 받아옴
            var nWaitPlaces = Int32.Parse(cmds[0]);
            var manager = new DeliveryVehicleManager(nWaitPlaces);    

            // 이후 명령어 반복문으로 처리
           for(int i = 1; i < cmds.Length; i++)
            {
                // 하나의 문장 가져오기
                var cmd = cmds[i];

                // 하나의 키워드 가져오기
                var keyword = cmd.Split(" ");

                // 명령어는 키워드의 첫자
                var op = keyword[0].Trim();
                if(op == "Quit")
                {
                    return;
                }
                
                // 아래는 그냥 하나하나 명령어 처리와 함수호출...
                else if(op == "ReadyIn")
                {
                    var waitPlaceId = Int32.Parse(keyword[1].Split("W")[1]);
                    var vehicleNumber = Int32.Parse(keyword[2]);
                    var addr = keyword[3];
                    var priority = Int32.Parse(keyword[4].Split("P")[1]);

                    manager.Readyin(vehicleNumber, addr, priority, waitPlaceId);
                }

                else if(op == "Ready")
                { 
                    var vehicleNumber = Int32.Parse(keyword[1]);
                    var addr = keyword[2];
                    var priority = Int32.Parse(keyword[3].Split("P")[1]);

                    manager.Ready(vehicleNumber, addr, priority);
                }

                else if (op == "Status")
                {
                    manager.PrintStatus();
                }

                else if (op == "Cancel")
                {
                    var vehicleNumber = Int32.Parse(keyword[1]);
                    manager.Cancel(vehicleNumber);
                }

                else if (op == "Deliver")
                {
                    var waitPlaceId = Int32.Parse(keyword[1].Split("W")[1]);
                    manager.Deliver(waitPlaceId);
                }

                else if(op == "Clear")
                {
                    var waitPlaceId = Int32.Parse(keyword[1].Split("W")[1]);
                    manager.Clear(waitPlaceId);
                }

                else
                {
                    throw new NotFoundException();
                }
            }  
        }
    }
}
