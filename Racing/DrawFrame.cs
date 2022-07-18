using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing
{
    public class DrawFrame
    {
        private char[,] pixel;
        private int width;
        private int height;

        private int playerPos;
        private int size;
        private int[] obstacleX;
        private int[] itemX;
        private int[] invincibleX;
        private int[] obstacleY;
        private int[] itemY;
        private int[] invincibleY;
        private Random random = new Random();

        public void InitDrawFrame() // 프레임 세팅 초기화
        {
            width = GameManager.Instance._settings.GetWidth();
            height = GameManager.Instance._settings.GetHeight();
            playerPos = width / 2; // 플레이어는 중앙에 위치
            pixel = new char[height, width]; // 화면 크기만큼 배열 생성
            size = 30; // 장애물 아이템의 공간 크기
            obstacleX = new int[size]; // 장애물 좌표
            itemX = new int[size]; // 아이템 좌표
            invincibleX = new int[size];
            obstacleY = new int[size];
            itemY = new int[size];
            invincibleY = new int[size];
            MakeObstacle(); // 장애물 생성
            MakeItem(); // 아이템 생성
            MakeInvincible(); // 무적 아이템 생성
        }

        public void DrawTile() // 화면 그리기
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0); // 커서 초기화
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Score : " + GameManager.Instance.GetScore()); // 점수 그리기
            Console.ResetColor();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (CheckObstacle(i,j)) // 장애물
                    {
                        Console.ForegroundColor = ConsoleColor.Black; // 색상 지정
                        Console.BackgroundColor = ConsoleColor.Gray;
                        pixel[i, j] = '■';
                        Console.Write(pixel[i, j]);
                        Console.ResetColor();
                    }
                    else if(CheckItem(i, j)) //아이템
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        pixel[i, j] = '♥';
                        Console.Write(pixel[i, j]);
                        Console.ResetColor();
                    }
                    else if(CheckInvincible(i, j))//무적아이템
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        pixel[i, j] = '★';
                        Console.Write(pixel[i, j]);
                        Console.ResetColor();
                    }
                    else if (j == playerPos && i == height - 1) // 플레이어
                    {
                        //일반상태일때
                        if (GameManager.Instance.invincibleFrame <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        pixel[i, j] = '◈';
                        Console.Write(pixel[i, j]);
                        Console.ResetColor();
                    }
                    else // 공백
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        pixel[i, j] = '　';
                        Console.Write(pixel[i, j]);
                        Console.ResetColor();
                    }
                    
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, 0); // 커서 초기화
            MoveObstacle(); // 장애물 이동
            MoveItem(); //점수 아이템 이동
            MoveInvincible(); //무적 아이템 이동
        }
        public void MakeObstacle() // 장애물 생성 함수
        {
            for (int i = 0; i < size; i += 3)
            {
                obstacleX[i] = random.Next(-height, 0);
                obstacleX[i + 1] = obstacleX[i];
                obstacleX[i + 2] = obstacleX[i];

                obstacleY[i] = random.Next(0, width);
                obstacleY[i + 1] = obstacleY[i] + 1;
                obstacleY[i + 2] = obstacleY[i] + 2;
            }
        }
        public void MakeItem()
        {
            for(int i = 0; i < size; i+= 10)
            {
                itemX[i] = random.Next(-height, 0);
                itemY[i] = random.Next(0, width);

            }
        }
        public void MakeInvincible()
        {
            for(int i = 0; i < size; i += 40)
            {
                invincibleX[i] = random.Next(-height, 0);
                invincibleY[i] = random.Next(0, width);
            }
        }


        public void MoveObstacle() // 장애물 이동함수
        {
            for(int i = 0; i < size; i+=3)
            {
                if (obstacleX[i] > height)
                {
                    obstacleX[i] = random.Next(-height, 0);
                    obstacleX[i + 1] = obstacleX[i];
                    obstacleX[i + 2] = obstacleX[i];

                    obstacleY[i] = random.Next(0, width);
                    obstacleY[i + 1] = obstacleY[i] + 1;
                    obstacleY[i + 2] = obstacleY[i] + 2;
                }
                else
                {
                    obstacleX[i]+=1;
                    obstacleX[i + 1] += 1;
                    obstacleX[i + 2] += 1;
                }
            }
        }

        public void MoveItem()
        {
            for(int i = 0; i < size; i+= 10)
            {
                if (itemX[i] > height)
                {
                    itemX[i] = random.Next(-height, 0);
                    itemY[i] = random.Next(0, width);
                }
                else
                {
                    itemX[i] += 1;
                }
            }
        }
        public void MoveInvincible()
        {
            for(int i = 0; i < size; i += 40)
            {
                if (invincibleX[i] > height)
                {
                    invincibleX[i] = random.Next(-height, 0);
                    invincibleY[i] = random.Next(0, width);
                }
                else
                {
                    invincibleX[i] += 1;
                }
            }
        }
        public void SetPlayerPos(int input) // 입력에 따른 캐릭터 이동
        {
            playerPos += input;
            if(playerPos < 0) playerPos = 0; // 범위제한
            else if(playerPos >= width) playerPos = width - 1;
        }
        public void ResetPlayerPos() // 플레이어 위치 초기화
        {
            playerPos = width / 2;
        }
        public bool CheckDead() // 사망 체크
        {
            if (CheckPlayerDead())
            {
                return true;
            }
            else return false;
        }
        public bool CheckScore()//점수 체크
        {
            if(CheckPlayerScore())
            {
                return true;
            }
            else return false;
        }
        public bool CheckBeInvincible()//무적 아이템 획득 체크
        {
            if (CheckPlayerInvincible())
            {
                return true;
            }
            else return false;
        }
        public void DrawDeadScene() // 종료화면
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("               Game Over                ");
            Console.WriteLine("         End : 0    Restart : 1         ");
            Console.WriteLine("              Score :{0:D4}               ", GameManager.Instance.GetScore());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }
        private bool CheckObstacle(int i, int j) // 장애물 위치 판정
        {
            for (int k = 0; k < size; k++)
            {
                if (i == obstacleX[k] && j == obstacleY[k])
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckItem(int i, int j)
        {
            for(int k = 0; k < size; k++)
            {
                if(i == itemX[k] && j == itemY[k])
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckInvincible(int i, int j)
        {
            for(int k = 0; k < size; k++)
            {
                if(i == invincibleX[k] && j == invincibleY[k])
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckPlayerDead() // 플레이어 충돌 판정
        {
            if(GameManager.Instance.invincibleFrame > 0) // 무적아이템이 적용중이라면
            {
                return false;
            }
            if(GameManager.Instance.GAMEOVER) // 게임 오버라면
            {
                return true;
            }
            for (int k = 0; k < size; k++) // 
            {
                if (playerPos == obstacleY[k] && height - 1 == obstacleX[k]) //플레이어 위치가 장애물
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckPlayerScore()  //플레이어 점수아이템 충돌 판정
        {
            for(int k = 0; k < size; k++)
            {
                if(playerPos == itemY[k] && height - 1 == itemX[k])
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckPlayerInvincible() //플레이어 무적아이템 충돌 판정
        {
            for(int k = 0; k < size; k++)
            {
                if(playerPos == invincibleY[k] && height - 1 == invincibleX[k])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
