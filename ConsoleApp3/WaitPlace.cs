using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class WaitPlace
    {
        public int id;
        public DeliveryVehicle[] vehicles;

        // vehicle 추가
        public DeliveryVehicle AddVehicle(int vNum, string addr, int priority)
        {
            // 1. vehicle 생성
            DeliveryVehicle newVehicle = new DeliveryVehicle { vehicleId = vNum, destination = addr, priority = priority };

            // 2. 기존의 배열을 지역변수로 옮기기
            DeliveryVehicle[] tmpVehicles = vehicles;

            // 3. 멤버 vehicles 의 길이 늘리기
            int newLength = tmpVehicles.Length + 1;
            vehicles = new DeliveryVehicle[newLength];

            // 4. priority 를 비교하며 배열에 vehicle 들을 추가한다.
            // 새로 생성된 vehicle 은 우선순위를 비교하며 다음 요소가 우선순위 값이 더 클때 직전에 넣는다.
            Boolean isNewAdded = false;
            int i = 0;
            foreach(var _v in tmpVehicles)
            {
                if(!isNewAdded && newVehicle.priority < _v.priority)
                {
                    isNewAdded = true;
                    vehicles[i] = newVehicle;
                    i++;
                }

                vehicles[i] = _v;
                i++;
            }
            if(!isNewAdded)
            {
                vehicles[i] = newVehicle;
            }

            // 5. 추가한 해당 vehicle 을 return 한다.
            return newVehicle;
        }

        // 차량번호를 이용해 vehicle 찾기
        public DeliveryVehicle FindVehicleByVNum(int vNum)
        {
            foreach(var _v in vehicles)
            {
                if(_v.vehicleId == vNum)
                {
                    return _v;
                }
            }
            return null;
        }


        public DeliveryVehicle Cancel(int vNum)
        {
            // 1. 배열에서 vNum 을 통해 Vehicle 을 찾아서 지역변수에 저장한다
            // 예외처리) vNum 에 해당하는 vehicle 없을 경우
            var cancelVehicle = FindVehicleByVNum(vNum);

            // 2. 크기가 하나 작은 Vehicle 배열 생성한다.
            var newVehiclesArr = new DeliveryVehicle[vehicles.Length - 1];

            // 3. vNum 에 해당하는 요소를 제외하고 배열에 담는다. (순서대로)
            int i = 0;
            foreach(var _v in vehicles)
            {
                if(_v == cancelVehicle)
                {
                    continue;
                }
                newVehiclesArr[i] = _v;
                i++;
            }
            vehicles = newVehiclesArr;

            // 4. cancel 한 vehicle 을 return 한다.
            return cancelVehicle;
        }

        public DeliveryVehicle Deliver()
        {
            // 1. 배열의 첫번째 vehicle 을 지역변수에 담는다.
            var departVehicle = vehicles[0];

            // 2. 기존의 배열을 지역변수로 옮기기
            var tmpVehiclesArr = vehicles;

            // 3. 멤버 vehicle 의 길이 줄이기
            vehicles = new DeliveryVehicle[vehicles.Length - 1];

            // 4. 첫번째 배열 vehicle를 제외하고 전부 배엘에 추가하기
            for(var i = 1; i < tmpVehiclesArr.Length; i++)
            {
                vehicles[i - 1] = tmpVehiclesArr[i];
            }

            // 5. 현재 배달을 한 vehicle 을 return 하기
            return departVehicle;
        }

        public void Clear()
        {
            vehicles = new DeliveryVehicle[0];
        }
    }
}
