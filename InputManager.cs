using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing
{
    public class InputManager : SingleTon<InputManager>
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int myKey);
        private short myKey = 0;
        public void GetKey()
        {
            myKey = 0;
            if (Console.KeyAvailable) // 키입력이 존재한다면
            {
                myKey = GetAsyncKeyState((int)ConsoleKey.RightArrow);
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameManager.Instance._frame.SetPlayerPos(1); // 우측 이동
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.LeftArrow);
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameManager.Instance._frame.SetPlayerPos(-1); // 좌측 이동
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.D0);
                if ((myKey & 0x8000) == 0x8000)
                {
                    if(GameManager.Instance.GAMEOVER) GameManager.Instance.endGame(); // 게임오버
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.D1);
                if ((myKey & 0x8000) == 0x8000)
                {
                    if (GameManager.Instance.GAMEOVER) GameManager.Instance.restartGame(); //게임 재시작
                }
            }
        }
    }
}
