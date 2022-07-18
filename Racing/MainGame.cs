using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing
{
    public class MainGame
    {
        private const double waitTick = 1000 / 60;

        public static void Main()
        {

            GameManager.Instance.InitGameManager(); // 기본 세팅 초기화

            long lastTick = 0; // 마지막 틱
            long currentTick; // 현재 틱

            while (true)// 반복
            {
                currentTick = System.Environment.TickCount & Int32.MaxValue; // 현재 시간
                if (currentTick - lastTick < waitTick)
                {
                    continue; // 경과 시간이 1 / 30 초 보다 작다면 실행 건너뜀
                }
                else
                {
                    lastTick = currentTick;
                    GameManager.Instance.Play(); // 플레이
                }
            }

        }
    }
}