using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing
{
    public class Settings
    {
        private int width;
        private int height;

        private int windowWidth;
        private int windowHeight;

        public void InitSetting() // 기본 세팅
        {
            width = 20;
            height = 40;
            windowWidth = 40 ;
            windowHeight = 42 ;
        }
        public void SetWindow() // 윈도우 설정
        {
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.BufferWidth = windowWidth;
            Console.BufferHeight = windowHeight;
        }

        public int GetWidth()
        {
            return width;
        }
        public int GetHeight()
        {
            return height;
        }
        public int GetWindowWidth()
        {
            return windowWidth;
        }
        public int GetWindowHeight()
        {
            return windowHeight;
        }
    }
}
