using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing
{
    public class GameManager : SingleTon<GameManager>
    {
        private int score = 0;
        public int invincibleFrame = 0;            //무적 남은시간
        public Settings _settings;
        public DrawFrame _frame;
        public bool GAMEOVER = false; // 게임 종료
        public GameManager()
        {
            _settings = new Settings();
            _frame = new DrawFrame();
        }

        public void InitGameManager()
        {
            _settings.InitSetting();
            _settings.SetWindow();
            _frame.InitDrawFrame();
        }
        public void Play()
        {
            InputManager.Instance.GetKey(); // 입력받기
            if (GAMEOVER)
            {
                return;
            }
            _frame.DrawTile(); // 그리기
            PlusScore(); // 점수 더하기

            if (invincibleFrame > 0)
            {
                invincibleFrame--;  //무적프레임 깎기
            }
            if (_frame.CheckDead()) // 사망 체크
            {
                GAMEOVER = true;
                _frame.DrawDeadScene();
            }

            if(_frame.CheckScore()) //점수아이템 체크
            {
                Console.Beep();
                score += 100;
            }

            if(_frame.CheckBeInvincible())//무적아이템 체크
            {
                Console.Beep();
                invincibleFrame += 30;
            }
        }

        public void restartGame()           //설정과 점수를 초기화하고 게임 다시시작.
        {
            GAMEOVER = false;
            _frame.MakeObstacle(); // 장애물 초기화
            ResetScore(); // 점수 초기화
            _frame.ResetPlayerPos(); // 플레이어 위치 초기화
        }

        public void endGame()               //게임 종료
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Environment.Exit(0);
        }
        public int GetScore()
        {
            return score;
        }
        public void PlusScore() // 점수 더하기
        {
            score++;
        }
        public void ResetScore() // 점수 초기화
        {
            score = 0;
        }
    }
}
