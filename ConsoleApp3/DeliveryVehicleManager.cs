using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class DeliveryVehicleManager
    {
        int numWaitingPlaces;
        WaitPlace[] waitPlaces;

        public DeliveryVehicleManager(int n)
        {
            numWaitingPlaces = n;
            waitPlaces = new WaitPlace[n];
            for (int i = 0; i < n; i++)
            {
                waitPlaces[i] = new WaitPlace { id = i + 1, vehicles = new DeliveryVehicle[0] };
            }
        }

        private WaitPlace findWaitPlaceWithId(int id)
        {
            // 1. id 에 해당하는 waitplace return
            foreach(var _wp in waitPlaces)
            {
                if(_wp.id == id)
                {
                    return _wp;
                }
            }
            throw new NotFoundException();
        }

        private WaitPlace findShortestWaitPlace() // vehicle 이 가장 적은 waitplace 반환
        {
            // 1. wait place 의 길이를 각각 확인한다
            WaitPlace wp = waitPlaces[0];
            foreach(var _wp in waitPlaces)
            {
                if(wp.vehicles.Length > _wp.vehicles.Length)
                {
                    wp = _wp;
                }
            }

            // 2. 가장 짧은 wait place 를 return 한다.
            return wp;
        }

        public void Readyin(int vNum, string addr, int priority, int waitPlaceId)
        {
            // 1. id 에 해당하는 wait place 를 받는다
            var wp = findWaitPlaceWithId(waitPlaceId);

            // 2. 해당 wait place 에 vehicle 을 추가한다.
            var v = wp.AddVehicle(vNum, addr, priority);

            // 3. 작업 내역 출력
            File.AppendAllText("./output.txt", $"Vehicle {v.vehicleId} assigned to WaitPlace #{wp.id}." + "\n");
        }

        public void Ready(int vNum, string addr, int priority)
        {
            // 1. 가장 짧은 wait place 를 받는다
            var wp = findShortestWaitPlace();

            // 2. 해당 wait place 에 vehicle 을 추가한다.
            var v = wp.AddVehicle(vNum, addr, priority);

            // 3. 작업 내역 출력
            File.AppendAllText("./output.txt", $"Vehicle {v.vehicleId} assigned to WaitPlace #{wp.id}." + "\n");
        }

        public void PrintStatus()
        {
            File.AppendAllText("./output.txt", $"************************ Delivery Vehicle Info ************************" + "\n");
            File.AppendAllText("./output.txt", $"Number of WaitPlaces: {numWaitingPlaces}" + "\n");
            foreach(var _p in waitPlaces)
            {
                File.AppendAllText("./output.txt", $"WaitPlace #{_p.id} Number Vehicles: {_p.vehicles.Length}" + "\n");
                foreach(var _v in _p.vehicles)
                {
                    File.AppendAllText("./output.txt", $"FNUM: {_v.vehicleId} DEST: {_v.destination} PRIO: {_v.priority}" + "\n");
                }
                File.AppendAllText("./output.txt", $"---------------------------------------------------" + "\n");
            }
            File.AppendAllText("./output.txt", $"********************** End Delivery Vehicle Info **********************" + "\n");
        }

        public void Cancel(int vNum)
        {
            // 1. 모든 wait place 에 vNum 에 해당하는 vehicle 이 있는지 확인한다
            WaitPlace wp = null;
            foreach(var _wp in waitPlaces)
            {
                if(_wp.FindVehicleByVNum(vNum) != null)
                {
                    wp = _wp;
                    break;
                }
            }
            if(wp == null)
            {
                throw new NotFoundException();
            }

            // 2. 해당 wait place 에서 vNum 에 해당하는 vehicle 을 삭제한다
            var v = wp.Cancel(vNum);

            // 3. 작업 내역 출력
            File.AppendAllText("./output.txt", $"Cancelation of Vehicle {v.vehicleId} completed." + "\n");
        }

        public void Deliver(int waitPlaceId)
        {
            // 1. 해당 wait place 찾기
            var wp = findWaitPlaceWithId(waitPlaceId);

            // 2. 배달하기
            var v = wp.Deliver();

            // 3. 작업내역 출력
            File.AppendAllText("./output.txt", $"Vehicle {v.vehicleId} assigned to WaitPlace #{wp.id}." + "\n");
        }

        public void Clear(int waitPlaceId)
        {
            // 1. 해당 wait place 를 모두 삭제한다
            var wp = findWaitPlaceWithId(waitPlaceId);

            // 2. clear 하기
            wp.Clear();

            // 3. 작업 내역 출력
            File.AppendAllText("./output.txt", $"WaitPlace #{wp.id} cleared.." + "\n");
        }
    }
}
