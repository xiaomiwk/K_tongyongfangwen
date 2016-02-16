namespace Utility.Windows
{
    public class Winuser
    {

        /*
         * Scroll Bar Constants
         */
        public const int SB_HORZ = 0;
        public const int SB_VERT = 1;
        public const int SB_CTL = 2;
        public const int SB_BOTH = 3;

        /*
         * Scroll Bar Commands
         */
        public const int SB_LINEUP = 0;
        public const int SB_LINELEFT = 0;
        public const int SB_LINEDOWN = 1;
        public const int SB_LINERIGHT = 1;
        public const int SB_PAGEUP = 2;
        public const int SB_PAGELEFT = 2;
        public const int SB_PAGEDOWN = 3;
        public const int SB_PAGERIGHT = 3;
        public const int SB_THUMBPOSITION = 4;
        public const int SB_THUMBTRACK = 5;
        public const int SB_TOP = 6;
        public const int SB_LEFT = 6;
        public const int SB_BOTTOM = 7;
        public const int SB_RIGHT = 7;
        public const int SB_ENDSCROLL = 8;


        /*
         * ShowWindow() Commands
         */
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;

        /*
         * Old ShowWindow() Commands
         */
        public const int HIDE_WINDOW = 0;
        public const int SHOW_OPENWINDOW = 1;
        public const int SHOW_ICONWINDOW = 2;
        public const int SHOW_FULLSCREEN = 3;
        public const int SHOW_OPENNOACTIVATE = 4;

        /*
         * Identifiers for the WM_SHOWWINDOW message
         */
        public const int SW_PARENTCLOSING = 1;
        public const int SW_OTHERZOOM = 2;
        public const int SW_PARENTOPENING = 3;
        public const int SW_OTHERUNZOOM = 4;


        //#if(WINVER >= 0x0500);
        /*
         * AnimateWindow() Commands
         */
        public const int AW_HOR_POSITIVE = 0x00000001;
        public const int AW_HOR_NEGATIVE = 0x00000002;
        public const int AW_VER_POSITIVE = 0x00000004;
        public const int AW_VER_NEGATIVE = 0x00000008;
        public const int AW_CENTER = 0x00000010;
        public const int AW_HIDE = 0x00010000;
        public const int AW_ACTIVATE = 0x00020000;
        public const int AW_SLIDE = 0x00040000;
        public const int AW_BLEND = 0x00080000;

        //#endif /* WINVER >= 0x0500 */


        /*
         * WM_KEYUP/DOWN/CHAR HIWORD(lParam) flags
         */
        public const int KF_EXTENDED = 0x0100;
        public const int KF_DLGMODE = 0x0800;
        public const int KF_MENUMODE = 0x1000;
        public const int KF_ALTDOWN = 0x2000;
        public const int KF_REPEAT = 0x4000;
        public const int KF_UP = 0x8000;

        //#ifndef NOVIRTUALKEYCODES


        /*
         * Virtual Keys, Standard Set
         */
        public const int VK_LBUTTON = 0x01;
        public const int VK_RBUTTON = 0x02;
        public const int VK_CANCEL = 0x03;
        public const int VK_MBUTTON = 0x04; /* NOT contiguous with L & RBUTTON */

        //#if(_WIN32_WINNT >= 0x0500);
        public const int VK_XBUTTON1 = 0x05;/* NOT contiguous with L & RBUTTON */
        public const int VK_XBUTTON2 = 0x06;/* NOT contiguous with L & RBUTTON */
        //#endif /* _WIN32_WINNT >= 0x0500 */

        /*
         * 0x07 : unassigned
         */

        public const int VK_BACK = 0x08;
        public const int VK_TAB = 0x09;

        /*
         * 0x0A - 0x0B : reserved
         */

        public const int VK_CLEAR = 0x0C;
        public const int VK_RETURN = 0x0D;

        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_PAUSE = 0x13;
        public const int VK_CAPITAL = 0x14;

        public const int VK_KANA = 0x15;
        public const int VK_HANGEUL = 0x15; /* old name - should be here for compatibility */
        public const int VK_HANGUL = 0x15;
        public const int VK_JUNJA = 0x17;
        public const int VK_FINAL = 0x18;
        public const int VK_HANJA = 0x19;
        public const int VK_KANJI = 0x19;

        public const int VK_ESCAPE = 0x1B;

        public const int VK_CONVERT = 0x1C;
        public const int VK_NONCONVERT = 0x1D;
        public const int VK_ACCEPT = 0x1E;
        public const int VK_MODECHANGE = 0x1F;

        public const int VK_SPACE = 0x20;
        public const int VK_PRIOR = 0x21;
        public const int VK_NEXT = 0x22;
        public const int VK_END = 0x23;
        public const int VK_HOME = 0x24;
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;
        public const int VK_SELECT = 0x29;
        public const int VK_PRINT = 0x2A;
        public const int VK_EXECUTE = 0x2B;
        public const int VK_SNAPSHOT = 0x2C;
        public const int VK_INSERT = 0x2D;
        public const int VK_DELETE = 0x2E;
        public const int VK_HELP = 0x2F;

        /*
         * VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
         * 0x40 : unassigned
         * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
         */

        public const int VK_LWIN = 0x5B;
        public const int VK_RWIN = 0x5C;
        public const int VK_APPS = 0x5D;

        /*
         * 0x5E : reserved
         */

        public const int VK_SLEEP = 0x5F;

        public const int VK_NUMPAD0 = 0x60;
        public const int VK_NUMPAD1 = 0x61;
        public const int VK_NUMPAD2 = 0x62;
        public const int VK_NUMPAD3 = 0x63;
        public const int VK_NUMPAD4 = 0x64;
        public const int VK_NUMPAD5 = 0x65;
        public const int VK_NUMPAD6 = 0x66;
        public const int VK_NUMPAD7 = 0x67;
        public const int VK_NUMPAD8 = 0x68;
        public const int VK_NUMPAD9 = 0x69;
        public const int VK_MULTIPLY = 0x6A;
        public const int VK_ADD = 0x6B;
        public const int VK_SEPARATOR = 0x6C;
        public const int VK_SUBTRACT = 0x6D;
        public const int VK_DECIMAL = 0x6E;
        public const int VK_DIVIDE = 0x6F;
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;
        public const int VK_F13 = 0x7C;
        public const int VK_F14 = 0x7D;
        public const int VK_F15 = 0x7E;
        public const int VK_F16 = 0x7F;
        public const int VK_F17 = 0x80;
        public const int VK_F18 = 0x81;
        public const int VK_F19 = 0x82;
        public const int VK_F20 = 0x83;
        public const int VK_F21 = 0x84;
        public const int VK_F22 = 0x85;
        public const int VK_F23 = 0x86;
        public const int VK_F24 = 0x87;

        /*
         * 0x88 - 0x8F : unassigned
         */

        public const int VK_NUMLOCK = 0x90;
        public const int VK_SCROLL = 0x91;

        /*
         * NEC PC-9800 kbd definitions
         */
        public const int VK_OEM_NEC_EQUAL = 0x92; // '=' key on numpad

        /*
         * Fujitsu/OASYS kbd definitions
         */
        public const int VK_OEM_FJ_JISHO = 0x92; // 'Dictionary' key
        public const int VK_OEM_FJ_MASSHOU = 0x93; // 'Unregister word' key
        public const int VK_OEM_FJ_TOUROKU = 0x94; // 'Register word' key
        public const int VK_OEM_FJ_LOYA = 0x95; // 'Left OYAYUBI' key
        public const int VK_OEM_FJ_ROYA = 0x96; // 'Right OYAYUBI' key

        /*
         * 0x97 - 0x9F : unassigned
         */

        /*
         * VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
         * Used only as parameters to GetAsyncKeyState() and GetKeyState().
         * No other API or message will distinguish left and right keys in this way.
         */
        public const int VK_LSHIFT = 0xA0;
        public const int VK_RSHIFT = 0xA1;
        public const int VK_LCONTROL = 0xA2;
        public const int VK_RCONTROL = 0xA3;
        public const int VK_LMENU = 0xA4;
        public const int VK_RMENU = 0xA5;

        //#if(_WIN32_WINNT >= 0x0500);
        public const int VK_BROWSER_BACK = 0xA6;
        public const int VK_BROWSER_FORWARD = 0xA7;
        public const int VK_BROWSER_REFRESH = 0xA8;
        public const int VK_BROWSER_STOP = 0xA9;
        public const int VK_BROWSER_SEARCH = 0xAA;
        public const int VK_BROWSER_FAVORITES = 0xAB;
        public const int VK_BROWSER_HOME = 0xAC;

        public const int VK_VOLUME_MUTE = 0xAD;
        public const int VK_VOLUME_DOWN = 0xAE;
        public const int VK_VOLUME_UP = 0xAF;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;
        public const int VK_MEDIA_STOP = 0xB2;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_LAUNCH_MAIL = 0xB4;
        public const int VK_LAUNCH_MEDIA_SELECT = 0xB5;
        public const int VK_LAUNCH_APP1 = 0xB6;
        public const int VK_LAUNCH_APP2 = 0xB7;

        //#endif /* _WIN32_WINNT >= 0x0500 */

        /*
         * 0xB8 - 0xB9 : reserved
         */

        public const int VK_OEM_1 = 0xBA; // ';:' for US
        public const int VK_OEM_PLUS = 0xBB; //  '+' any country
        public const int VK_OEM_COMMA = 0xBC; //  ',' any country
        public const int VK_OEM_MINUS = 0xBD; //  '-' any country
        public const int VK_OEM_PERIOD = 0xBE; //  '.' any country
        public const int VK_OEM_2 = 0xBF; //  '/?' for US
        public const int VK_OEM_3 = 0xC0; //  '`~' for US

        /*
         * 0xC1 - 0xD7 : reserved
         */

        /*
         * 0xD8 - 0xDA : unassigned
         */

        public const int VK_OEM_4 = 0xDB; // '[{' for US
        public const int VK_OEM_5 = 0xDC; // '\|' for US
        public const int VK_OEM_6 = 0xDD; // ']}' for US
        public const int VK_OEM_7 = 0xDE; // ''"' for US
        public const int VK_OEM_8 = 0xDF;

        /*
         * 0xE0 : reserved
         */

        /*
         * Various extended or enhanced keyboards
         */
        public const int VK_OEM_AX = 0xE1; // 'AX' key on Japanese AX kbd
        public const int VK_OEM_102 = 0xE2; // "<>" or "\|" on RT 102-key kbd.
        public const int VK_ICO_HELP = 0xE3; // Help key on ICO
        public const int VK_ICO_00 = 0xE4; // 00 key on ICO

        //#if(WINVER >= 0x0400);
        public const int VK_PROCESSKEY = 0xE5;
        //#endif /* WINVER >= 0x0400 */

        public const int VK_ICO_CLEAR = 0xE6;


        //#if(_WIN32_WINNT >= 0x0500);
        public const int VK_PACKET = 0xE7;
        //#endif /* _WIN32_WINNT >= 0x0500 */

        /*
         * 0xE8 : unassigned
         */

        /*
         * Nokia/Ericsson definitions
         */
        public const int VK_OEM_RESET = 0xE9;
        public const int VK_OEM_JUMP = 0xEA;
        public const int VK_OEM_PA1 = 0xEB;
        public const int VK_OEM_PA2 = 0xEC;
        public const int VK_OEM_PA3 = 0xED;
        public const int VK_OEM_WSCTRL = 0xEE;
        public const int VK_OEM_CUSEL = 0xEF;
        public const int VK_OEM_ATTN = 0xF0;
        public const int VK_OEM_FINISH = 0xF1;
        public const int VK_OEM_COPY = 0xF2;
        public const int VK_OEM_AUTO = 0xF3;
        public const int VK_OEM_ENLW = 0xF4;
        public const int VK_OEM_BACKTAB = 0xF5;

        public const int VK_ATTN = 0xF6;
        public const int VK_CRSEL = 0xF7;
        public const int VK_EXSEL = 0xF8;
        public const int VK_EREOF = 0xF9;
        public const int VK_PLAY = 0xFA;
        public const int VK_ZOOM = 0xFB;
        public const int VK_NONAME = 0xFC;
        public const int VK_PA1 = 0xFD;
        public const int VK_OEM_CLEAR = 0xFE;

        /*
         * 0xFF : reserved
         */


        //#endif /* !NOVIRTUALKEYCODES */

        //#ifndef NOWH

        /*
         * SetWindowsHook() codes
         */
        public const int WH_MIN = (-1);
        public const int WH_MSGFILTER = (-1);
        public const int WH_JOURNALRECORD = 0;
        public const int WH_JOURNALPLAYBACK = 1;
        public const int WH_KEYBOARD = 2;
        public const int WH_GETMESSAGE = 3;
        public const int WH_CALLWNDPROC = 4;
        public const int WH_CBT = 5;
        public const int WH_SYSMSGFILTER = 6;
        public const int WH_MOUSE = 7;
        //#if defined(_WIN32_WINDOWS)
        public const int WH_HARDWARE = 8;
        //#endif
        public const int WH_DEBUG = 9;
        public const int WH_SHELL = 10;
        public const int WH_FOREGROUNDIDLE = 11;
        //#if(WINVER >= 0x0400);
        public const int WH_CALLWNDPROCRET = 12;
        //#endif /* WINVER >= 0x0400 */

        //#if (_WIN32_WINNT >= 0x0400);
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;
        //#endif // (_WIN32_WINNT >= 0x0400);

        //#if(WINVER >= 0x0400);
        //#if (_WIN32_WINNT >= 0x0400);
        //public const int WH_MAX = 14;
        //#else
        public const int WH_MAX = 12;
        //#endif // (_WIN32_WINNT >= 0x0400);
        //#else
        //public const int WH_MAX = 11;
        //#endif

        public const int WH_MINHOOK = WH_MIN;
        public const int WH_MAXHOOK = WH_MAX;

        /*
         * Hook Codes
         */
        public const int HC_ACTION = 0;
        public const int HC_GETNEXT = 1;
        public const int HC_SKIP = 2;
        public const int HC_NOREMOVE = 3;
        public const int HC_NOREM = HC_NOREMOVE;
        public const int HC_SYSMODALON = 4;
        public const int HC_SYSMODALOFF = 5;

        /*
         * CBT Hook Codes
         */
        public const int HCBT_MOVESIZE = 0;
        public const int HCBT_MINMAX = 1;
        public const int HCBT_QS = 2;
        public const int HCBT_CREATEWND = 3;
        public const int HCBT_DESTROYWND = 4;
        public const int HCBT_ACTIVATE = 5;
        public const int HCBT_CLICKSKIPPED = 6;
        public const int HCBT_KEYSKIPPED = 7;
        public const int HCBT_SYSCOMMAND = 8;
        public const int HCBT_SETFOCUS = 9;


        /*
         * codes passed in WPARAM for WM_WTSSESSION_CHANGE
         */

        public const int WTS_CONSOLE_CONNECT = 0x1;
        public const int WTS_CONSOLE_DISCONNECT = 0x2;
        public const int WTS_REMOTE_CONNECT = 0x3;
        public const int WTS_REMOTE_DISCONNECT = 0x4;
        public const int WTS_SESSION_LOGON = 0x5;
        public const int WTS_SESSION_LOGOFF = 0x6;
        public const int WTS_SESSION_LOCK = 0x7;
        public const int WTS_SESSION_UNLOCK = 0x8;
        public const int WTS_SESSION_REMOTE_CONTROL = 0x9;

        //#endif /* _WIN32_WINNT >= 0x0501 */

        /*
         * WH_MSGFILTER Filter Proc Codes
         */
        public const int MSGF_DIALOGBOX = 0;
        public const int MSGF_MESSAGEBOX = 1;
        public const int MSGF_MENU = 2;
        public const int MSGF_SCROLLBAR = 5;
        public const int MSGF_NEXTWINDOW = 6;
        public const int MSGF_MAX = 8; //  unused
        public const int MSGF_USER = 4096;

        /*
         * Shell support
         */
        public const int HSHELL_WINDOWCREATED = 1;
        public const int HSHELL_WINDOWDESTROYED = 2;
        public const int HSHELL_ACTIVATESHELLWINDOW = 3;

        //#if(WINVER >= 0x0400);
        public const int HSHELL_WINDOWACTIVATED = 4;
        public const int HSHELL_GETMINRECT = 5;
        public const int HSHELL_REDRAW = 6;
        public const int HSHELL_TASKMAN = 7;
        public const int HSHELL_LANGUAGE = 8;
        public const int HSHELL_SYSMENU = 9;
        public const int HSHELL_ENDTASK = 10;
        //#endif /* WINVER >= 0x0400 */
        //#if(_WIN32_WINNT >= 0x0500);
        public const int HSHELL_ACCESSIBILITYSTATE = 11;
        public const int HSHELL_APPCOMMAND = 12;
        //#endif /* _WIN32_WINNT >= 0x0500 */

        //#if(_WIN32_WINNT >= 0x0501);
        public const int HSHELL_WINDOWREPLACED = 13;
        public const int HSHELL_WINDOWREPLACING = 14;
        //#endif /* _WIN32_WINNT >= 0x0501 */


        public const int HSHELL_HIGHBIT = 0x8000;
        public const int HSHELL_FLASH = (HSHELL_REDRAW | HSHELL_HIGHBIT);
        public const int HSHELL_RUDEAPPACTIVATED = (HSHELL_WINDOWACTIVATED | HSHELL_HIGHBIT);

        //#if(_WIN32_WINNT >= 0x0500);
        /* wparam for HSHELL_ACCESSIBILITYSTATE */
        public const int ACCESS_STICKYKEYS = 0x0001;
        public const int ACCESS_FILTERKEYS = 0x0002;
        public const int ACCESS_MOUSEKEYS = 0x0003;

        /* cmd for HSHELL_APPCOMMAND and WM_APPCOMMAND */
        public const int APPCOMMAND_BROWSER_BACKWARD = 1;
        public const int APPCOMMAND_BROWSER_FORWARD = 2;
        public const int APPCOMMAND_BROWSER_REFRESH = 3;
        public const int APPCOMMAND_BROWSER_STOP = 4;
        public const int APPCOMMAND_BROWSER_SEARCH = 5;
        public const int APPCOMMAND_BROWSER_FAVORITES = 6;
        public const int APPCOMMAND_BROWSER_HOME = 7;
        public const int APPCOMMAND_VOLUME_MUTE = 8;
        public const int APPCOMMAND_VOLUME_DOWN = 9;
        public const int APPCOMMAND_VOLUME_UP = 10;
        public const int APPCOMMAND_MEDIA_NEXTTRACK = 11;
        public const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 12;
        public const int APPCOMMAND_MEDIA_STOP = 13;
        public const int APPCOMMAND_MEDIA_PLAY_PAUSE = 14;
        public const int APPCOMMAND_LAUNCH_MAIL = 15;
        public const int APPCOMMAND_LAUNCH_MEDIA_SELECT = 16;
        public const int APPCOMMAND_LAUNCH_APP1 = 17;
        public const int APPCOMMAND_LAUNCH_APP2 = 18;
        public const int APPCOMMAND_BASS_DOWN = 19;
        public const int APPCOMMAND_BASS_BOOST = 20;
        public const int APPCOMMAND_BASS_UP = 21;
        public const int APPCOMMAND_TREBLE_DOWN = 22;
        public const int APPCOMMAND_TREBLE_UP = 23;
        //#if(_WIN32_WINNT >= 0x0501);
        public const int APPCOMMAND_MICROPHONE_VOLUME_MUTE = 24;
        public const int APPCOMMAND_MICROPHONE_VOLUME_DOWN = 25;
        public const int APPCOMMAND_MICROPHONE_VOLUME_UP = 26;
        public const int APPCOMMAND_HELP = 27;
        public const int APPCOMMAND_FIND = 28;
        public const int APPCOMMAND_NEW = 29;
        public const int APPCOMMAND_OPEN = 30;
        public const int APPCOMMAND_CLOSE = 31;
        public const int APPCOMMAND_SAVE = 32;
        public const int APPCOMMAND_PRINT = 33;
        public const int APPCOMMAND_UNDO = 34;
        public const int APPCOMMAND_REDO = 35;
        public const int APPCOMMAND_COPY = 36;
        public const int APPCOMMAND_CUT = 37;
        public const int APPCOMMAND_PASTE = 38;
        public const int APPCOMMAND_REPLY_TO_MAIL = 39;
        public const int APPCOMMAND_FORWARD_MAIL = 40;
        public const int APPCOMMAND_SEND_MAIL = 41;
        public const int APPCOMMAND_SPELL_CHECK = 42;
        public const int APPCOMMAND_DICTATE_OR_COMMAND_CONTROL_TOGGLE = 43;
        public const int APPCOMMAND_MIC_ON_OFF_TOGGLE = 44;
        public const int APPCOMMAND_CORRECTION_LIST = 45;
        public const int APPCOMMAND_MEDIA_PLAY = 46;
        public const int APPCOMMAND_MEDIA_PAUSE = 47;
        public const int APPCOMMAND_MEDIA_RECORD = 48;
        public const int APPCOMMAND_MEDIA_FAST_FORWARD = 49;
        public const int APPCOMMAND_MEDIA_REWIND = 50;
        public const int APPCOMMAND_MEDIA_CHANNEL_UP = 51;
        public const int APPCOMMAND_MEDIA_CHANNEL_DOWN = 52;
        //#endif /* _WIN32_WINNT >= 0x0501 */

        public const int FAPPCOMMAND_MOUSE = 0x8000;
        public const int FAPPCOMMAND_KEY = 0;
        public const int FAPPCOMMAND_OEM = 0x1000;
        public const int FAPPCOMMAND_MASK = 0xF000;

        //#endif /* _WIN32_WINNT >= 0x0500 */


        /*
         * Keyboard Layout API
         */
        public const int HKL_PREV = 0;
        public const int HKL_NEXT = 1;


        public const int KLF_ACTIVATE = 0x00000001;
        public const int KLF_SUBSTITUTE_OK = 0x00000002;
        public const int KLF_REORDER = 0x00000008;
        //#if(WINVER >= 0x0400);
        public const int KLF_REPLACELANG = 0x00000010;
        public const int KLF_NOTELLSHELL = 0x00000080;
        //#endif /* WINVER >= 0x0400 */
        public const int KLF_SETFORPROCESS = 0x00000100;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int KLF_SHIFTLOCK = 0x00010000;
        public const int KLF_RESET = 0x40000000;
        //#endif /* _WIN32_WINNT >= 0x0500 */


        //#if(WINVER >= 0x0500);
        /*
         * Bits in wParam of WM_INPUTLANGCHANGEREQUEST message
         */
        public const int INPUTLANGCHANGE_SYSCHARSET = 0x0001;
        public const int INPUTLANGCHANGE_FORWARD = 0x0002;
        public const int INPUTLANGCHANGE_BACKWARD = 0x0004;
        //#endif /* WINVER >= 0x0500 */


        /*
         * Size of KeyboardLayoutName (number of characters), including nul terminator
         */
        public const int KL_NAMELENGTH = 9;


        /*
         * Values for resolution parameter of GetMouseMovePointsEx
         */
        public const int GMMP_USE_DISPLAY_POINTS = 1;
        public const int GMMP_USE_HIGH_RESOLUTION_POINTS = 2;


        /*
         * Desktop-specific access flags
         */
        public const int DESKTOP_READOBJECTS = 0x0001;
        public const int DESKTOP_CREATEWINDOW = 0x0002;
        public const int DESKTOP_CREATEMENU = 0x0004;
        public const int DESKTOP_HOOKCONTROL = 0x0008;
        public const int DESKTOP_JOURNALRECORD = 0x0010;
        public const int DESKTOP_JOURNALPLAYBACK = 0x0020;
        public const int DESKTOP_ENUMERATE = 0x0040;
        public const int DESKTOP_WRITEOBJECTS = 0x0080;
        public const int DESKTOP_SWITCHDESKTOP = 0x0100;

        /*
         * Desktop-specific control flags
         */
        public const int DF_ALLOWOTHERACCOUNTHOOK = 0x0001;


        /*
         * Windowstation-specific access flags
         */
        public const int WINSTA_ENUMDESKTOPS = 0x0001;
        public const int WINSTA_READATTRIBUTES = 0x0002;
        public const int WINSTA_ACCESSCLIPBOARD = 0x0004;
        public const int WINSTA_CREATEDESKTOP = 0x0008;
        public const int WINSTA_WRITEATTRIBUTES = 0x0010;
        public const int WINSTA_ACCESSGLOBALATOMS = 0x0020;
        public const int WINSTA_EXITWINDOWS = 0x0040;
        public const int WINSTA_ENUMERATE = 0x0100;
        public const int WINSTA_READSCREEN = 0x0200;

        public const int WINSTA_ALL_ACCESS = (WINSTA_ENUMDESKTOPS | WINSTA_READATTRIBUTES | WINSTA_ACCESSCLIPBOARD | WINSTA_CREATEDESKTOP | WINSTA_WRITEATTRIBUTES | WINSTA_ACCESSGLOBALATOMS | WINSTA_EXITWINDOWS | WINSTA_ENUMERATE | WINSTA_READSCREEN);

        /*
         * Windowstation creation flags.
         */
        public const int CWF_CREATE_ONLY = 0x0001;

        /*
         * Windowstation-specific attribute flags
         */
        public const int WSF_VISIBLE = 0x0001;


        public const int UOI_FLAGS = 1;
        public const int UOI_NAME = 2;
        public const int UOI_TYPE = 3;
        public const int UOI_USER_SID = 4;


        /*
         * Window field offsets for GetWindowLong()
         */
        public const int GWL_WNDPROC = (-4);
        public const int GWL_HINSTANCE = (-6);
        public const int GWL_HWNDPARENT = (-8);
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);
        public const int GWL_USERDATA = (-21);
        public const int GWL_ID = (-12);


        public const int GWLP_WNDPROC = (-4);
        public const int GWLP_HINSTANCE = (-6);
        public const int GWLP_HWNDPARENT = (-8);
        public const int GWLP_USERDATA = (-21);
        public const int GWLP_ID = (-12);


        /*
         * Class field offsets for GetClassLong()
         */
        public const int GCL_MENUNAME = (-8);
        public const int GCL_HBRBACKGROUND = (-10);
        public const int GCL_HCURSOR = (-12);
        public const int GCL_HICON = (-14);
        public const int GCL_HMODULE = (-16);
        public const int GCL_CBWNDEXTRA = (-18);
        public const int GCL_CBCLSEXTRA = (-20);
        public const int GCL_WNDPROC = (-24);
        public const int GCL_STYLE = (-26);
        public const int GCW_ATOM = (-32);


        //#if(WINVER >= 0x0400);
        public const int GCL_HICONSM = (-34);
        //#endif /* WINVER >= 0x0400 */



        public const int GCLP_MENUNAME = (-8);
        public const int GCLP_HBRBACKGROUND = (-10);
        public const int GCLP_HCURSOR = (-12);
        public const int GCLP_HICON = (-14);
        public const int GCLP_HMODULE = (-16);
        public const int GCLP_WNDPROC = (-24);
        public const int GCLP_HICONSM = (-34);

        public const uint UINT_MAX = 0xffffffff;//limit.h

        public const int WM_NULL = 0x0000;
        public const int WM_CREATE = 0x0001;
        public const int WM_DESTROY = 0x0002;
        public const int WM_MOVE = 0x0003;
        public const int WM_SIZE = 0x0005;

        public const int WM_ACTIVATE = 0x0006;
        /*;
         * WM_ACTIVATE state values;
         */
        public const int WM_SETFOCUS = 0x0007;
        public const int WM_KILLFOCUS = 0x0008;
        public const int WM_ENABLE = 0x000A;
        public const int WM_SETREDRAW = 0x000B;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;
        public const int WM_PAINT = 0x000F;
        public const int WM_CLOSE = 0x0010;
        public const int WM_QUERYENDSESSION = 0x0011;
        public const int WM_QUIT = 0x0012;
        public const int WM_QUERYOPEN = 0x0013;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_SYSCOLORCHANGE = 0x0015;
        public const int WM_ENDSESSION = 0x0016;
        public const int WM_SHOWWINDOW = 0x0018;
        public const int WM_WININICHANGE = 0x001A;
        //#if(WINVER >= 0x0400);
        public const int WM_SETTINGCHANGE = WM_WININICHANGE;
        public const int WM_DEVMODECHANGE = 0x001B;
        public const int WM_ACTIVATEAPP = 0x001C;
        public const int WM_FONTCHANGE = 0x001D;
        public const int WM_TIMECHANGE = 0x001E;
        public const int WM_CANCELMODE = 0x001F;
        public const int WM_SETCURSOR = 0x0020;
        public const int WM_MOUSEACTIVATE = 0x0021;
        public const int WM_CHILDACTIVATE = 0x0022;
        public const int WM_QUEUESYNC = 0x0023;
        public const int WM_GETMINMAXINFO = 0x0024;
        // end_r_winuser;
        // begin_r_winuser;
        public const int WM_PAINTICON = 0x0026;
        public const int WM_ICONERASEBKGND = 0x0027;
        public const int WM_NEXTDLGCTL = 0x0028;
        public const int WM_SPOOLERSTATUS = 0x002A;
        public const int WM_DRAWITEM = 0x002B;
        public const int WM_MEASUREITEM = 0x002C;
        public const int WM_DELETEITEM = 0x002D;
        public const int WM_VKEYTOITEM = 0x002E;
        public const int WM_CHARTOITEM = 0x002F;
        public const int WM_SETFONT = 0x0030;
        public const int WM_GETFONT = 0x0031;
        public const int WM_SETHOTKEY = 0x0032;
        public const int WM_GETHOTKEY = 0x0033;
        public const int WM_QUERYDRAGICON = 0x0037;
        public const int WM_COMPAREITEM = 0x0039;
        //#if(WINVER >= 0x0500);
        public const int WM_GETOBJECT = 0x003D;
        //#endif /* WINVER >= 0x0500 */;
        public const int WM_COMPACTING = 0x0041;
        public const int WM_COMMNOTIFY = 0x0044; /* no longer suported */
        public const int WM_WINDOWPOSCHANGING = 0x0046;
        public const int WM_WINDOWPOSCHANGED = 0x0047;

        public const int WM_POWER = 0x0048;
        /*;
         * wParam for WM_POWER window message and DRV_POWER driver notification;
         */
        public const int PWR_OK = 1;
        public const int PWR_FAIL = (-1);
        public const int PWR_SUSPENDREQUEST = 1;
        public const int PWR_SUSPENDRESUME = 2;
        public const int PWR_CRITICALRESUME = 3;

        public const int WM_COPYDATA = 0x004A;
        public const int WM_CANCELJOURNAL = 0x004B;

        // end_r_winuser;

        /*;
         * lParam of WM_COPYDATA message points to...;
         */

        // begin_r_winuser;

        //#if(WINVER >= 0x0400);
        public const int WM_NOTIFY = 0x004E;
        public const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
        public const int WM_INPUTLANGCHANGE = 0x0051;
        public const int WM_TCARD = 0x0052;
        public const int WM_HELP = 0x0053;
        public const int WM_USERCHANGED = 0x0054;
        public const int WM_NOTIFYFORMAT = 0x0055;

        public const int NFR_ANSI = 1;
        public const int NFR_UNICODE = 2;
        public const int NF_QUERY = 3;
        public const int NF_REQUERY = 4;

        public const int WM_CONTEXTMENU = 0x007B;
        public const int WM_STYLECHANGING = 0x007C;
        public const int WM_STYLECHANGED = 0x007D;
        public const int WM_DISPLAYCHANGE = 0x007E;
        public const int WM_GETICON = 0x007F;
        public const int WM_SETICON = 0x0080;
        //#endif /* WINVER >= 0x0400 */;

        public const int WM_NCCREATE = 0x0081;
        public const int WM_NCDESTROY = 0x0082;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_GETDLGCODE = 0x0087;
        public const int WM_SYNCPAINT = 0x0088;
        public const int WM_NCMOUSEMOVE = 0x00A0;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;
        public const int WM_NCRBUTTONDOWN = 0x00A4;
        public const int WM_NCRBUTTONUP = 0x00A5;
        public const int WM_NCRBUTTONDBLCLK = 0x00A6;
        public const int WM_NCMBUTTONDOWN = 0x00A7;
        public const int WM_NCMBUTTONUP = 0x00A8;
        public const int WM_NCMBUTTONDBLCLK = 0x00A9;

        public const int WM_KEYFIRST = 0x0100;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CHAR = 0x0102;
        public const int WM_DEADCHAR = 0x0103;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_SYSCHAR = 0x0106;
        public const int WM_SYSDEADCHAR = 0x0107;
        public const int WM_KEYLAST = 0x0108;

        //#if(WINVER >= 0x0400);
        public const int WM_IME_STARTCOMPOSITION = 0x010D;
        public const int WM_IME_ENDCOMPOSITION = 0x010E;
        public const int WM_IME_COMPOSITION = 0x010F;
        public const int WM_IME_KEYLAST = 0x010F;
        //#endif /* WINVER >= 0x0400 */

        public const int WM_INITDIALOG = 0x0110;
        public const int WM_COMMAND = 0x0111;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_TIMER = 0x0113;
        public const int WM_HSCROLL = 0x0114;
        public const int WM_VSCROLL = 0x0115;
        public const int WM_INITMENU = 0x0116;
        public const int WM_INITMENUPOPUP = 0x0117;
        public const int WM_MENUSELECT = 0x011F;
        public const int WM_MENUCHAR = 0x0120;
        public const int WM_ENTERIDLE = 0x0121;
        //#if(WINVER >= 0x0500);
        public const int WM_MENURBUTTONUP = 0x0122;
        public const int WM_MENUDRAG = 0x0123;
        public const int WM_MENUGETOBJECT = 0x0124;
        public const int WM_UNINITMENUPOPUP = 0x0125;
        public const int WM_MENUCOMMAND = 0x0126;
        //#endif /* WINVER >= 0x0500 */;


        public const int WM_CTLCOLORMSGBOX = 0x0132;
        public const int WM_CTLCOLOREDIT = 0x0133;
        public const int WM_CTLCOLORLISTBOX = 0x0134;
        public const int WM_CTLCOLORBTN = 0x0135;
        public const int WM_CTLCOLORDLG = 0x0136;
        public const int WM_CTLCOLORSCROLLBAR = 0x0137;
        public const int WM_CTLCOLORSTATIC = 0x0138;


        public const int WM_MOUSEFIRST = 0x0200;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;

        //#if (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400);
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_MOUSELAST = 0x020A;
        //#else;
        //public const int WM_MOUSELAST = 0x0209;
        //#endif /* if (_WIN32_WINNT < 0x0400) */;

        //#if(_WIN32_WINNT >= 0x0400);
        public const int WHEEL_DELTA = 120;/* Value for rolling one detent */
        //#endif /* _WIN32_WINNT >= 0x0400 */;
        //#if(_WIN32_WINNT >= 0x0400);
        public const uint WHEEL_PAGESCROLL = (UINT_MAX); /* Scroll one page */
        //#endif /* _WIN32_WINNT >= 0x0400 */;

        public const int WM_PARENTNOTIFY = 0x0210;
        public const int WM_ENTERMENULOOP = 0x0211;
        public const int WM_EXITMENULOOP = 0x0212;

        //#if(WINVER >= 0x0400);
        public const int WM_NEXTMENU = 0x0213;
        // end_r_winuser;

        // begin_r_winuser;
        public const int WM_SIZING = 0x0214;
        public const int WM_CAPTURECHANGED = 0x0215;
        public const int WM_MOVING = 0x0216;
        // end_r_winuser;
        public const int WM_POWERBROADCAST = 0x0218;// r_winuser pbt;
        // begin_pbt;

        public const int PBT_APMQUERYSUSPEND = 0x0000;
        public const int PBT_APMQUERYSTANDBY = 0x0001;

        public const int PBT_APMQUERYSUSPENDFAILED = 0x0002;
        public const int PBT_APMQUERYSTANDBYFAILED = 0x0003;

        public const int PBT_APMSUSPEND = 0x0004;
        public const int PBT_APMSTANDBY = 0x0005;

        public const int PBT_APMRESUMECRITICAL = 0x0006;
        public const int PBT_APMRESUMESUSPEND = 0x0007;
        public const int PBT_APMRESUMESTANDBY = 0x0008;

        public const int PBTF_APMRESUMEFROMFAILURE = 0x00000001;

        public const int PBT_APMBATTERYLOW = 0x0009;
        public const int PBT_APMPOWERSTATUSCHANGE = 0x000A;

        public const int PBT_APMOEMEVENT = 0x000B;
        public const int PBT_APMRESUMEAUTOMATIC = 0x0012;
        // end_pbt;

        // begin_r_winuser;
        public const int WM_DEVICECHANGE = 0x0219;

        //#endif /* WINVER >= 0x0400 */;

        public const int WM_MDICREATE = 0x0220;
        public const int WM_MDIDESTROY = 0x0221;
        public const int WM_MDIACTIVATE = 0x0222;
        public const int WM_MDIRESTORE = 0x0223;
        public const int WM_MDINEXT = 0x0224;
        public const int WM_MDIMAXIMIZE = 0x0225;
        public const int WM_MDITILE = 0x0226;
        public const int WM_MDICASCADE = 0x0227;
        public const int WM_MDIICONARRANGE = 0x0228;
        public const int WM_MDIGETACTIVE = 0x0229;


        public const int WM_MDISETMENU = 0x0230;
        public const int WM_ENTERSIZEMOVE = 0x0231;
        public const int WM_EXITSIZEMOVE = 0x0232;
        public const int WM_DROPFILES = 0x0233;
        public const int WM_MDIREFRESHMENU = 0x0234;


        //#if(WINVER >= 0x0400);
        public const int WM_IME_SETCONTEXT = 0x0281;
        public const int WM_IME_NOTIFY = 0x0282;
        public const int WM_IME_CONTROL = 0x0283;
        public const int WM_IME_COMPOSITIONFULL = 0x0284;
        public const int WM_IME_SELECT = 0x0285;
        public const int WM_IME_CHAR = 0x0286;
        //#endif /* WINVER >= 0x0400 */;
        //#if(WINVER >= 0x0500);
        public const int WM_IME_REQUEST = 0x0288;
        //#endif /* WINVER >= 0x0500 */;
        //#if(WINVER >= 0x0400);
        public const int WM_IME_KEYDOWN = 0x0290;
        public const int WM_IME_KEYUP = 0x0291;
        //#endif /* WINVER >= 0x0400 */;


        //#if(_WIN32_WINNT >= 0x0400);
        public const int WM_MOUSEHOVER = 0x02A1;
        public const int WM_MOUSELEAVE = 0x02A3;
        //#endif /* _WIN32_WINNT >= 0x0400 */;

        public const int WM_NCMOUSEHOVER = 0x02A0;
        public const int WM_NCMOUSELEAVE = 0x02A2;

        public const int WM_CUT = 0x0300;
        public const int WM_COPY = 0x0301;
        public const int WM_PASTE = 0x0302;
        public const int WM_CLEAR = 0x0303;
        public const int WM_UNDO = 0x0304;
        public const int WM_RENDERFORMAT = 0x0305;
        public const int WM_RENDERALLFORMATS = 0x0306;
        public const int WM_DESTROYCLIPBOARD = 0x0307;
        public const int WM_DRAWCLIPBOARD = 0x0308;
        public const int WM_PAINTCLIPBOARD = 0x0309;
        public const int WM_VSCROLLCLIPBOARD = 0x030A;
        public const int WM_SIZECLIPBOARD = 0x030B;
        public const int WM_ASKCBFORMATNAME = 0x030C;
        public const int WM_CHANGECBCHAIN = 0x030D;
        public const int WM_HSCROLLCLIPBOARD = 0x030E;
        public const int WM_QUERYNEWPALETTE = 0x030F;
        public const int WM_PALETTEISCHANGING = 0x0310;
        public const int WM_PALETTECHANGED = 0x0311;
        public const int WM_HOTKEY = 0x0312;

        //#if(WINVER >= 0x0400);
        public const int WM_PRINT = 0x0317;
        public const int WM_PRINTCLIENT = 0x0318;

        public const int WM_HANDHELDFIRST = 0x0358;
        public const int WM_HANDHELDLAST = 0x035F;

        public const int WM_AFXFIRST = 0x0360;
        public const int WM_AFXLAST = 0x037F;
        //#endif /* WINVER >= 0x0400 */;

        public const int WM_PENWINFIRST = 0x0380;
        public const int WM_PENWINLAST = 0x038F;


        //#if(WINVER >= 0x0400);
        public const int WM_APP = 0x8000;
        //#endif /* WINVER >= 0x0400 */;

        /*;
         * NOTE: All Message Numbers below 0x0400 are RESERVED.;
         *;
         * Private Window Messages Start Here:;
         */
        public const int WM_USER = 0x0400;

        /* = wParam for WM_SIZING message = */
        public const int WMSZ_LEFT = 1;
        public const int WMSZ_RIGHT = 2;
        public const int WMSZ_TOP = 3;
        public const int WMSZ_TOPLEFT = 4;
        public const int WMSZ_TOPRIGHT = 5;
        public const int WMSZ_BOTTOM = 6;
        public const int WMSZ_BOTTOMLEFT = 7;
        public const int WMSZ_BOTTOMRIGHT = 8;
        //#endif /* WINVER >= 0x0400 */


        /*
         * WM_NCHITTEST and MOUSEHOOKSTRUCT Mouse Position Codes
         */
        public const int HTERROR = (-2);
        public const int HTTRANSPARENT = (-1);
        public const int HTNOWHERE = 0;
        public const int HTCLIENT = 1;
        public const int HTCAPTION = 2;
        public const int HTSYSMENU = 3;
        public const int HTGROWBOX = 4;
        public const int HTSIZE = HTGROWBOX;
        public const int HTMENU = 5;
        public const int HTHSCROLL = 6;
        public const int HTVSCROLL = 7;
        public const int HTMINBUTTON = 8;
        public const int HTMAXBUTTON = 9;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTBORDER = 18;
        public const int HTREDUCE = HTMINBUTTON;
        public const int HTZOOM = HTMAXBUTTON;
        public const int HTSIZEFIRST = HTLEFT;
        public const int HTSIZELAST = HTBOTTOMRIGHT;
        //#if(WINVER >= 0x0400);
        public const int HTOBJECT = 19;
        public const int HTCLOSE = 20;
        public const int HTHELP = 21;
        //#endif /* WINVER >= 0x0400 */


        /*
         * SendMessageTimeout values
         */
        public const int SMTO_NORMAL = 0x0000;
        public const int SMTO_BLOCK = 0x0001;
        public const int SMTO_ABORTIFHUNG = 0x0002;
        //#if(WINVER >= 0x0500);
        public const int SMTO_NOTIMEOUTIFNOTHUNG = 0x0008;
        //#endif /* WINVER >= 0x0500 */
        //#endif /* !NONCMESSAGES */

        /*
         * WM_MOUSEACTIVATE Return Codes
         */
        public const int MA_ACTIVATE = 1;
        public const int MA_ACTIVATEANDEAT = 2;
        public const int MA_NOACTIVATE = 3;
        public const int MA_NOACTIVATEANDEAT = 4;

        /*
         * WM_SETICON / WM_GETICON Type Codes
         */
        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        //#if(_WIN32_WINNT >= 0x0501);
        public const int ICON_SMALL2 = 2;
        //#endif /* _WIN32_WINNT >= 0x0501 */



        /*
         * WM_SIZE message wParam values
         */
        public const int SIZE_RESTORED = 0;
        public const int SIZE_MINIMIZED = 1;
        public const int SIZE_MAXIMIZED = 2;
        public const int SIZE_MAXSHOW = 3;
        public const int SIZE_MAXHIDE = 4;

        /*
         * Obsolete constant names
         */
        public const int SIZENORMAL = SIZE_RESTORED;
        public const int SIZEICONIC = SIZE_MINIMIZED;
        public const int SIZEFULLSCREEN = SIZE_MAXIMIZED;
        public const int SIZEZOOMSHOW = SIZE_MAXSHOW;
        public const int SIZEZOOMHIDE = SIZE_MAXHIDE;


        /*
         * WM_NCCALCSIZE "window valid rect" return values
         */
        public const int WVR_ALIGNTOP = 0x0010;
        public const int WVR_ALIGNLEFT = 0x0020;
        public const int WVR_ALIGNBOTTOM = 0x0040;
        public const int WVR_ALIGNRIGHT = 0x0080;
        public const int WVR_HREDRAW = 0x0100;
        public const int WVR_VREDRAW = 0x0200;
        public const int WVR_REDRAW = (WVR_HREDRAW | WVR_VREDRAW);
        public const int WVR_VALIDRECTS = 0x0400;


        /*
         * Key State Masks for Mouse Messages
         */
        public const int MK_LBUTTON = 0x0001;
        public const int MK_RBUTTON = 0x0002;
        public const int MK_SHIFT = 0x0004;
        public const int MK_CONTROL = 0x0008;
        public const int MK_MBUTTON = 0x0010;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int MK_XBUTTON1 = 0x0020;
        public const int MK_XBUTTON2 = 0x0040;
        //#endif /* _WIN32_WINNT >= 0x0500 */

        //#endif /* !NOKEYSTATES */


        //#if(_WIN32_WINNT >= 0x0400);
        //#ifndef NOTRACKMOUSEEVENT

        public const int TME_HOVER = 0x00000001;
        public const int TME_LEAVE = 0x00000002;
        //#if(WINVER >= 0x0500);
        public const int TME_NONCLIENT = 0x00000010;
        //#endif /* WINVER >= 0x0500 */
        public const int TME_QUERY = 0x40000000;
        public const uint TME_CANCEL = 0x80000000;


        public const uint HOVER_DEFAULT = 0xFFFFFFFF;
        //#endif /* _WIN32_WINNT >= 0x0400 */


        /*
         * Window Styles
         */
        public const int WS_OVERLAPPED = 0x00000000;
        public const uint WS_POPUP = 0x80000000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_DISABLED = 0x08000000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int WS_MAXIMIZE = 0x01000000;
        public const int WS_CAPTION = 0x00C00000; /* WS_BORDER | WS_DLGFRAME = */
        public const int WS_BORDER = 0x00800000;
        public const int WS_DLGFRAME = 0x00400000;
        public const int WS_VSCROLL = 0x00200000;
        public const int WS_HSCROLL = 0x00100000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000;
        public const int WS_GROUP = 0x00020000;
        public const int WS_TABSTOP = 0x00010000;

        public const int WS_MINIMIZEBOX = 0x00020000;
        public const int WS_MAXIMIZEBOX = 0x00010000;


        public const int WS_TILED = WS_OVERLAPPED;
        public const int WS_ICONIC = WS_MINIMIZE;
        public const int WS_SIZEBOX = WS_THICKFRAME;
        public const int WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

        /*
         * Common Window Styles
         */
        public const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);

        public const uint WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU);

        public const int WS_CHILDWINDOW = (WS_CHILD);

        /*
         * Extended Window Styles
         */
        public const int WS_EX_DLGMODALFRAME = 0x00000001;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_ACCEPTFILES = 0x00000010;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        //#if(WINVER >= 0x0400);
        public const int WS_EX_MDICHILD = 0x00000040;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_WINDOWEDGE = 0x00000100;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_CONTEXTHELP = 0x00000400;

        //#endif /* WINVER >= 0x0400 */
        //#if(WINVER >= 0x0400);

        public const int WS_EX_RIGHT = 0x00001000;
        public const int WS_EX_LEFT = 0x00000000;
        public const int WS_EX_RTLREADING = 0x00002000;
        public const int WS_EX_LTRREADING = 0x00000000;
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;

        public const int WS_EX_CONTROLPARENT = 0x00010000;
        public const int WS_EX_STATICEDGE = 0x00020000;
        public const int WS_EX_APPWINDOW = 0x00040000;


        public const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);

        //#endif /* WINVER >= 0x0400 */

        //#if(_WIN32_WINNT >= 0x0500);
        public const int WS_EX_LAYERED = 0x00080000;

        //#endif /* _WIN32_WINNT >= 0x0500 */


        //#if(WINVER >= 0x0500);
        public const int WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        public const int WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        //#endif /* WINVER >= 0x0500 */

        //#if(_WIN32_WINNT >= 0x0501);
        public const int WS_EX_COMPOSITED = 0x02000000;
        //#endif /* _WIN32_WINNT >= 0x0501 */
        //#if(_WIN32_WINNT >= 0x0500);
        public const int WS_EX_NOACTIVATE = 0x08000000;
        //#endif /* _WIN32_WINNT >= 0x0500 */


        /*
         * Class styles
         */
        public const int CS_VREDRAW = 0x0001;
        public const int CS_HREDRAW = 0x0002;
        public const int CS_DBLCLKS = 0x0008;
        public const int CS_OWNDC = 0x0020;
        public const int CS_CLASSDC = 0x0040;
        public const int CS_PARENTDC = 0x0080;
        public const int CS_NOCLOSE = 0x0200;
        public const int CS_SAVEBITS = 0x0800;
        public const int CS_BYTEALIGNCLIENT = 0x1000;
        public const int CS_BYTEALIGNWINDOW = 0x2000;
        public const int CS_GLOBALCLASS = 0x4000;

        public const int CS_IME = 0x00010000;
        //#if(_WIN32_WINNT >= 0x0501);
        public const int CS_DROPSHADOW = 0x00020000;
        //#endif /* _WIN32_WINNT >= 0x0501 */



        //#endif /* !NOWINSTYLES */
        //#if(WINVER >= 0x0400);
        /* WM_PRINT flags */
        public const int PRF_CHECKVISIBLE = 0x00000001;
        public const int PRF_NONCLIENT = 0x00000002;
        public const int PRF_CLIENT = 0x00000004;
        public const int PRF_ERASEBKGND = 0x00000008;
        public const int PRF_CHILDREN = 0x00000010;
        public const int PRF_OWNED = 0x00000020;

        /* 3D border styles */
        public const int BDR_RAISEDOUTER = 0x0001;
        public const int BDR_SUNKENOUTER = 0x0002;
        public const int BDR_RAISEDINNER = 0x0004;
        public const int BDR_SUNKENINNER = 0x0008;

        public const int BDR_OUTER = (BDR_RAISEDOUTER | BDR_SUNKENOUTER);
        public const int BDR_INNER = (BDR_RAISEDINNER | BDR_SUNKENINNER);
        public const int BDR_RAISED = (BDR_RAISEDOUTER | BDR_RAISEDINNER);
        public const int BDR_SUNKEN = (BDR_SUNKENOUTER | BDR_SUNKENINNER);


        public const int EDGE_RAISED = (BDR_RAISEDOUTER | BDR_RAISEDINNER);
        public const int EDGE_SUNKEN = (BDR_SUNKENOUTER | BDR_SUNKENINNER);
        public const int EDGE_ETCHED = (BDR_SUNKENOUTER | BDR_RAISEDINNER);
        public const int EDGE_BUMP = (BDR_RAISEDOUTER | BDR_SUNKENINNER);

        /* Border flags */
        public const int BF_LEFT = 0x0001;
        public const int BF_TOP = 0x0002;
        public const int BF_RIGHT = 0x0004;
        public const int BF_BOTTOM = 0x0008;

        public const int BF_TOPLEFT = (BF_TOP | BF_LEFT);
        public const int BF_TOPRIGHT = (BF_TOP | BF_RIGHT);
        public const int BF_BOTTOMLEFT = (BF_BOTTOM | BF_LEFT);
        public const int BF_BOTTOMRIGHT = (BF_BOTTOM | BF_RIGHT);
        public const int BF_RECT = (BF_LEFT | BF_TOP | BF_RIGHT | BF_BOTTOM);

        public const int BF_DIAGONAL = 0x0010;

        // For diagonal lines, the BF_RECT flags specify the end point of the
        // vector bounded by the rectangle parameter.
        public const int BF_DIAGONAL_ENDTOPRIGHT = (BF_DIAGONAL | BF_TOP | BF_RIGHT);
        public const int BF_DIAGONAL_ENDTOPLEFT = (BF_DIAGONAL | BF_TOP | BF_LEFT);
        public const int BF_DIAGONAL_ENDBOTTOMLEFT = (BF_DIAGONAL | BF_BOTTOM | BF_LEFT);
        public const int BF_DIAGONAL_ENDBOTTOMRIGHT = (BF_DIAGONAL | BF_BOTTOM | BF_RIGHT);


        public const int BF_MIDDLE = 0x0800; /* Fill in the middle */
        public const int BF_SOFT = 0x1000; /* For softer buttons */
        public const int BF_ADJUST = 0x2000; /* Calculate the space left over */
        public const int BF_FLAT = 0x4000; /* For flat rather than 3D borders */
        public const int BF_MONO = 0x8000; /* For monochrome borders */



        /* flags for DrawFrameControl */

        public const int DFC_CAPTION = 1;
        public const int DFC_MENU = 2;
        public const int DFC_SCROLL = 3;
        public const int DFC_BUTTON = 4;
        //#if(WINVER >= 0x0500);
        public const int DFC_POPUPMENU = 5;
        //#endif /* WINVER >= 0x0500 */

        public const int DFCS_CAPTIONCLOSE = 0x0000;
        public const int DFCS_CAPTIONMIN = 0x0001;
        public const int DFCS_CAPTIONMAX = 0x0002;
        public const int DFCS_CAPTIONRESTORE = 0x0003;
        public const int DFCS_CAPTIONHELP = 0x0004;

        public const int DFCS_MENUARROW = 0x0000;
        public const int DFCS_MENUCHECK = 0x0001;
        public const int DFCS_MENUBULLET = 0x0002;
        public const int DFCS_MENUARROWRIGHT = 0x0004;
        public const int DFCS_SCROLLUP = 0x0000;
        public const int DFCS_SCROLLDOWN = 0x0001;
        public const int DFCS_SCROLLLEFT = 0x0002;
        public const int DFCS_SCROLLRIGHT = 0x0003;
        public const int DFCS_SCROLLCOMBOBOX = 0x0005;
        public const int DFCS_SCROLLSIZEGRIP = 0x0008;
        public const int DFCS_SCROLLSIZEGRIPRIGHT = 0x0010;

        public const int DFCS_BUTTONCHECK = 0x0000;
        public const int DFCS_BUTTONRADIOIMAGE = 0x0001;
        public const int DFCS_BUTTONRADIOMASK = 0x0002;
        public const int DFCS_BUTTONRADIO = 0x0004;
        public const int DFCS_BUTTON3STATE = 0x0008;
        public const int DFCS_BUTTONPUSH = 0x0010;

        public const int DFCS_INACTIVE = 0x0100;
        public const int DFCS_PUSHED = 0x0200;
        public const int DFCS_CHECKED = 0x0400;

        //#if(WINVER >= 0x0500);
        public const int DFCS_TRANSPARENT = 0x0800;
        public const int DFCS_HOT = 0x1000;
        //#endif /* WINVER >= 0x0500 */

        public const int DFCS_ADJUSTRECT = 0x2000;
        public const int DFCS_FLAT = 0x4000;
        public const int DFCS_MONO = 0x8000;


        /* flags for DrawCaption */
        public const int DC_ACTIVE = 0x0001;
        public const int DC_SMALLCAP = 0x0002;
        public const int DC_ICON = 0x0004;
        public const int DC_TEXT = 0x0008;
        public const int DC_INBUTTON = 0x0010;
        //#if(WINVER >= 0x0500);
        public const int DC_GRADIENT = 0x0020;
        //#endif /* WINVER >= 0x0500 */
        //#if(_WIN32_WINNT >= 0x0501);
        public const int DC_BUTTONS = 0x1000;
        //#endif /* _WIN32_WINNT >= 0x0501 */




        public const int IDANI_OPEN = 1;
        public const int IDANI_CAPTION = 3;



        //#endif /* WINVER >= 0x0400 */

        //#ifndef NOCLIPBOARD


        /*
         * Predefined Clipboard Formats
         */
        public const int CF_TEXT = 1;
        public const int CF_BITMAP = 2;
        public const int CF_METAFILEPICT = 3;
        public const int CF_SYLK = 4;
        public const int CF_DIF = 5;
        public const int CF_TIFF = 6;
        public const int CF_OEMTEXT = 7;
        public const int CF_DIB = 8;
        public const int CF_PALETTE = 9;
        public const int CF_PENDATA = 10;
        public const int CF_RIFF = 11;
        public const int CF_WAVE = 12;
        public const int CF_UNICODETEXT = 13;
        public const int CF_ENHMETAFILE = 14;
        //#if(WINVER >= 0x0400);
        public const int CF_HDROP = 15;
        public const int CF_LOCALE = 16;
        //#endif /* WINVER >= 0x0400 */
        //#if(WINVER >= 0x0500);
        public const int CF_DIBV5 = 17;
        //#endif /* WINVER >= 0x0500 */

        //..WINVER
        //#if(WINVER >= 0x0500);
        public const int CF_MAX = 18;
        //#elif(WINVER >= 0x0400);
        //public const int CF_MAX = 17;
        ////#else
        //public const int CF_MAX = 15;
        //#endif

        public const int CF_OWNERDISPLAY = 0x0080;
        public const int CF_DSPTEXT = 0x0081;
        public const int CF_DSPBITMAP = 0x0082;
        public const int CF_DSPMETAFILEPICT = 0x0083;
        public const int CF_DSPENHMETAFILE = 0x008E;

        /*
         * "Private" formats don't get GlobalFree()'d
         */
        public const int CF_PRIVATEFIRST = 0x0200;
        public const int CF_PRIVATELAST = 0x02FF;

        /*
         * "GDIOBJ" formats do get DeleteObject()'d
         */
        public const int CF_GDIOBJFIRST = 0x0300;
        public const int CF_GDIOBJLAST = 0x03FF;


        //#endif /* !NOCLIPBOARD */

        /*
         * Defines for the fVirt field of the Accelerator table structure.
         */
        public const int FVIRTKEY = 1; /* Assumed to be == TRUE */
        public const int FNOINVERT = 0x02;
        public const int FSHIFT = 0x04;
        public const int FCONTROL = 0x08;
        public const int FALT = 0x10;


        public const int WPF_SETMINPOSITION = 0x0001;
        public const int WPF_RESTORETOMAXIMIZED = 0x0002;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int WPF_ASYNCWINDOWPLACEMENT = 0x0004;
        //#endif /* _WIN32_WINNT >= 0x0500 */


        /*
         * Owner draw control types
         */
        public const int ODT_MENU = 1;
        public const int ODT_LISTBOX = 2;
        public const int ODT_COMBOBOX = 3;
        public const int ODT_BUTTON = 4;
        //#if(WINVER >= 0x0400);
        public const int ODT_STATIC = 5;
        //#endif /* WINVER >= 0x0400 */

        /*
         * Owner draw actions
         */
        public const int ODA_DRAWENTIRE = 0x0001;
        public const int ODA_SELECT = 0x0002;
        public const int ODA_FOCUS = 0x0004;

        /*
         * Owner draw state
         */
        public const int ODS_SELECTED = 0x0001;
        public const int ODS_GRAYED = 0x0002;
        public const int ODS_DISABLED = 0x0004;
        public const int ODS_CHECKED = 0x0008;
        public const int ODS_FOCUS = 0x0010;
        //#if(WINVER >= 0x0400);
        public const int ODS_DEFAULT = 0x0020;
        public const int ODS_COMBOBOXEDIT = 0x1000;
        //#endif /* WINVER >= 0x0400 */
        //#if(WINVER >= 0x0500);
        public const int ODS_HOTLIGHT = 0x0040;
        public const int ODS_INACTIVE = 0x0080;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int ODS_NOACCEL = 0x0100;
        public const int ODS_NOFOCUSRECT = 0x0200;
        //#endif /* _WIN32_WINNT >= 0x0500 */
        //#endif /* WINVER >= 0x0500 */

        //#endif /* WINVER >= 0x0500 */

        /*
         * PeekMessage() Options
         */
        public const int PM_NOREMOVE = 0x0000;
        public const int PM_REMOVE = 0x0001;
        public const int PM_NOYIELD = 0x0002;
        //#if(WINVER >= 0x0500);
        public const int PM_QS_INPUT = (QS_INPUT << 16);
        public const int PM_QS_POSTMESSAGE = ((QS_POSTMESSAGE | QS_HOTKEY | QS_TIMER) << 16);
        public const int PM_QS_PAINT = (QS_PAINT << 16);
        public const int PM_QS_SENDMESSAGE = (QS_SENDMESSAGE << 16);
        //#endif /* WINVER >= 0x0500 */



        public const int MOD_ALT = 0x0001;
        public const int MOD_CONTROL = 0x0002;
        public const int MOD_SHIFT = 0x0004;
        public const int MOD_WIN = 0x0008;


        public const int IDHOT_SNAPWINDOW = (-1); /* SHIFT-PRINTSCRN = */
        public const int IDHOT_SNAPDESKTOP = (-2); /* PRINTSCRN = */


        public const uint ENDSESSION_LOGOFF = 0x80000000;


        public const int EWX_LOGOFF = 0;
        public const int EWX_SHUTDOWN = 0x00000001;
        public const int EWX_REBOOT = 0x00000002;
        public const int EWX_FORCE = 0x00000004;
        public const int EWX_POWEROFF = 0x00000008;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int EWX_FORCEIFHUNG = 0x00000010;
        //#endif /* _WIN32_WINNT >= 0x0500 */


        //Broadcast Special Message Recipient list
        public const int BSM_ALLCOMPONENTS = 0x00000000;
        public const int BSM_VXDS = 0x00000001;
        public const int BSM_NETDRIVER = 0x00000002;
        public const int BSM_INSTALLABLEDRIVERS = 0x00000004;
        public const int BSM_APPLICATIONS = 0x00000008;
        public const int BSM_ALLDESKTOPS = 0x00000010;

        //Broadcast Special Message Flags
        public const int BSF_QUERY = 0x00000001;
        public const int BSF_IGNORECURRENTTASK = 0x00000002;
        public const int BSF_FLUSHDISK = 0x00000004;
        public const int BSF_NOHANG = 0x00000008;
        public const int BSF_POSTMESSAGE = 0x00000010;
        public const int BSF_FORCEIFHUNG = 0x00000020;
        public const int BSF_NOTIMEOUTIFNOTHUNG = 0x00000040;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int BSF_ALLOWSFW = 0x00000080;
        public const int BSF_SENDNOTIFYMESSAGE = 0x00000100;
        //#endif /* _WIN32_WINNT >= 0x0500 */
        //#if(_WIN32_WINNT >= 0x0501);
        public const int BSF_RETURNHDESK = 0x00000200;
        public const int BSF_LUID = 0x00000400;
        //#endif /* _WIN32_WINNT >= 0x0501 */

        public const int BROADCAST_QUERY_DENY = 0x424D5144; // Return this value to deny a query.
        //#endif /* WINVER >= 0x0400 */

        // RegisterDeviceNotification

        //#if(WINVER >= 0x0500);
        //typedef = PVOID = HDEVNOTIFY;
        //typedef = HDEVNOTIFY = *PHDEVNOTIFY;

        public const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;
        public const int DEVICE_NOTIFY_SERVICE_HANDLE = 0x00000001;
        //#if(_WIN32_WINNT >= 0x0501);
        public const int DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 0x00000004;
        //#endif /* _WIN32_WINNT >= 0x0501 */



        /*
         * Special HWND value for use with PostMessage() and SendMessage()
         */
        public const int HWND_BROADCAST = (0xffff);

        //#if(WINVER >= 0x0500);
        public const int HWND_MESSAGE = (-3);
        //#endif /* WINVER >= 0x0500 */


        /*
         * InSendMessageEx return value
         */
        public const int ISMEX_NOSEND = 0x00000000;
        public const int ISMEX_SEND = 0x00000001;
        public const int ISMEX_NOTIFY = 0x00000002;
        public const int ISMEX_CALLBACK = 0x00000004;
        public const int ISMEX_REPLIED = 0x00000008;
        //#endif /* WINVER >= 0x0500 */


        public const uint CW_USEDEFAULT = 0x80000000;


        /*
         * Special value for CreateWindow, et al.
         */
        public const int HWND_DESKTOP = (0);


        public const int PW_CLIENTONLY = 0x00000001;


        public const int LWA_COLORKEY = 0x00000001;
        public const int LWA_ALPHA = 0x00000002;


        public const int ULW_COLORKEY = 0x00000001;
        public const int ULW_ALPHA = 0x00000002;
        public const int ULW_OPAQUE = 0x00000004;

        public const int ULW_EX_NORESIZE = 0x00000008;


        public const int FLASHW_STOP = 0;
        public const int FLASHW_CAPTION = 0x00000001;
        public const int FLASHW_TRAY = 0x00000002;
        public const int FLASHW_ALL = (FLASHW_CAPTION | FLASHW_TRAY);
        public const int FLASHW_TIMER = 0x00000004;
        public const int FLASHW_TIMERNOFG = 0x0000000C;


        /*
         * SetWindowPos Flags
         */
        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOREDRAW = 0x0008;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_FRAMECHANGED = 0x0020; /* The frame changed: send WM_NCCALCSIZE */
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_HIDEWINDOW = 0x0080;
        public const int SWP_NOCOPYBITS = 0x0100;
        public const int SWP_NOOWNERZORDER = 0x0200; /* Don't do owner Z ordering */
        public const int SWP_NOSENDCHANGING = 0x0400; /* Don't send WM_WINDOWPOSCHANGING */

        public const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;

        //#if(WINVER >= 0x0400);
        public const int SWP_DEFERERASE = 0x2000;
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        //#endif /* WINVER >= 0x0400 */


        public const int HWND_TOP = (0);
        public const int HWND_BOTTOM = (1);
        public const int HWND_TOPMOST = (-1);
        public const int HWND_NOTOPMOST = (-2);


        /*
         * Window extra byted needed for private dialog classes.
         */
        //#ifndef _MAC
        public const int DLGWINDOWEXTRA = 30;
        //#else
        //public const int DLGWINDOWEXTRA = 48;
        //#endif


        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const int KEYEVENTF_KEYUP = 0x0002;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int KEYEVENTF_UNICODE = 0x0004;
        public const int KEYEVENTF_SCANCODE = 0x0008;
        //#endif /* _WIN32_WINNT >= 0x0500 */


        public const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        public const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        public const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; /* middle button down */
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040; /* middle button up */
        public const int MOUSEEVENTF_XDOWN = 0x0080; /* x button down */
        public const int MOUSEEVENTF_XUP = 0x0100; /* x button down */
        public const int MOUSEEVENTF_WHEEL = 0x0800; /* wheel button rolled */
        public const int MOUSEEVENTF_VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000; /* absolute move */




        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;


        public const int MWMO_WAITALL = 0x0001;
        public const int MWMO_ALERTABLE = 0x0002;
        public const int MWMO_INPUTAVAILABLE = 0x0004;


        /*
         * Queue status flags for GetQueueStatus() and MsgWaitForMultipleObjects()
         */
        public const int QS_KEY = 0x0001;
        public const int QS_MOUSEMOVE = 0x0002;
        public const int QS_MOUSEBUTTON = 0x0004;
        public const int QS_POSTMESSAGE = 0x0008;
        public const int QS_TIMER = 0x0010;
        public const int QS_PAINT = 0x0020;
        public const int QS_SENDMESSAGE = 0x0040;
        public const int QS_HOTKEY = 0x0080;
        public const int QS_ALLPOSTMESSAGE = 0x0100;
        ////#if(_WIN32_WINNT >= 0x0501);
        public const int QS_RAWINPUT = 0x0400;
        ////#endif /* _WIN32_WINNT >= 0x0501 */

        public const int QS_MOUSE = (QS_MOUSEMOVE | QS_MOUSEBUTTON);

        //..WINVER
        ////#if (_WIN32_WINNT >= 0x0501);
        public const int QS_INPUT = (QS_MOUSE | QS_KEY | QS_RAWINPUT);
        ////#else
        //public const int QS_INPUT = (QS_MOUSE | QS_KEY);
        ////#endif // (_WIN32_WINNT >= 0x0501);

        public const int QS_ALLEVENTS = (QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY);

        public const int QS_ALLINPUT = (QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY | QS_SENDMESSAGE);


        public const int USER_TIMER_MAXIMUM = 0x7FFFFFFF;
        public const int USER_TIMER_MINIMUM = 0x0000000A;


        /*
         * GetSystemMetrics() codes
         */

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const int SM_CXVSCROLL = 2;
        public const int SM_CYHSCROLL = 3;
        public const int SM_CYCAPTION = 4;
        public const int SM_CXBORDER = 5;
        public const int SM_CYBORDER = 6;
        public const int SM_CXDLGFRAME = 7;
        public const int SM_CYDLGFRAME = 8;
        public const int SM_CYVTHUMB = 9;
        public const int SM_CXHTHUMB = 10;
        public const int SM_CXICON = 11;
        public const int SM_CYICON = 12;
        public const int SM_CXCURSOR = 13;
        public const int SM_CYCURSOR = 14;
        public const int SM_CYMENU = 15;
        public const int SM_CXFULLSCREEN = 16;
        public const int SM_CYFULLSCREEN = 17;
        public const int SM_CYKANJIWINDOW = 18;
        public const int SM_MOUSEPRESENT = 19;
        public const int SM_CYVSCROLL = 20;
        public const int SM_CXHSCROLL = 21;
        public const int SM_DEBUG = 22;
        public const int SM_SWAPBUTTON = 23;
        public const int SM_RESERVED1 = 24;
        public const int SM_RESERVED2 = 25;
        public const int SM_RESERVED3 = 26;
        public const int SM_RESERVED4 = 27;
        public const int SM_CXMIN = 28;
        public const int SM_CYMIN = 29;
        public const int SM_CXSIZE = 30;
        public const int SM_CYSIZE = 31;
        public const int SM_CXFRAME = 32;
        public const int SM_CYFRAME = 33;
        public const int SM_CXMINTRACK = 34;
        public const int SM_CYMINTRACK = 35;
        public const int SM_CXDOUBLECLK = 36;
        public const int SM_CYDOUBLECLK = 37;
        public const int SM_CXICONSPACING = 38;
        public const int SM_CYICONSPACING = 39;
        public const int SM_MENUDROPALIGNMENT = 40;
        public const int SM_PENWINDOWS = 41;
        public const int SM_DBCSENABLED = 42;
        public const int SM_CMOUSEBUTTONS = 43;

        ////#if(WINVER >= 0x0400);
        public const int SM_CXFIXEDFRAME = SM_CXDLGFRAME; /* ;win40 name change */
        public const int SM_CYFIXEDFRAME = SM_CYDLGFRAME; /* ;win40 name change */
        public const int SM_CXSIZEFRAME = SM_CXFRAME; /* ;win40 name change */
        public const int SM_CYSIZEFRAME = SM_CYFRAME; /* ;win40 name change */

        public const int SM_SECURE = 44;
        public const int SM_CXEDGE = 45;
        public const int SM_CYEDGE = 46;
        public const int SM_CXMINSPACING = 47;
        public const int SM_CYMINSPACING = 48;
        public const int SM_CXSMICON = 49;
        public const int SM_CYSMICON = 50;
        public const int SM_CYSMCAPTION = 51;
        public const int SM_CXSMSIZE = 52;
        public const int SM_CYSMSIZE = 53;
        public const int SM_CXMENUSIZE = 54;
        public const int SM_CYMENUSIZE = 55;
        public const int SM_ARRANGE = 56;
        public const int SM_CXMINIMIZED = 57;
        public const int SM_CYMINIMIZED = 58;
        public const int SM_CXMAXTRACK = 59;
        public const int SM_CYMAXTRACK = 60;
        public const int SM_CXMAXIMIZED = 61;
        public const int SM_CYMAXIMIZED = 62;
        public const int SM_NETWORK = 63;
        public const int SM_CLEANBOOT = 67;
        public const int SM_CXDRAG = 68;
        public const int SM_CYDRAG = 69;
        ////#endif /* WINVER >= 0x0400 */
        public const int SM_SHOWSOUNDS = 70;
        ////#if(WINVER >= 0x0400);
        public const int SM_CXMENUCHECK = 71; /* Use instead of GetMenuCheckMarkDimensions()! */
        public const int SM_CYMENUCHECK = 72;
        public const int SM_SLOWMACHINE = 73;
        public const int SM_MIDEASTENABLED = 74;
        ////#endif /* WINVER >= 0x0400 */

        ////#if (WINVER >= 0x0500) || (_WIN32_WINNT >= 0x0400);
        public const int SM_MOUSEWHEELPRESENT = 75;
        ////#endif
        ////#if(WINVER >= 0x0500);
        public const int SM_XVIRTUALSCREEN = 76;
        public const int SM_YVIRTUALSCREEN = 77;
        public const int SM_CXVIRTUALSCREEN = 78;
        public const int SM_CYVIRTUALSCREEN = 79;
        public const int SM_CMONITORS = 80;
        public const int SM_SAMEDISPLAYFORMAT = 81;
        ////#endif /* WINVER >= 0x0500 */
        ////#if(_WIN32_WINNT >= 0x0500);
        public const int SM_IMMENABLED = 82;
        ////#endif /* _WIN32_WINNT >= 0x0500 */
        ////#if(_WIN32_WINNT >= 0x0501);
        public const int SM_CXFOCUSBORDER = 83;
        public const int SM_CYFOCUSBORDER = 84;
        ////#endif /* _WIN32_WINNT >= 0x0501 */

        ////#if(_WIN32_WINNT >= 0x0501);
        public const int SM_TABLETPC = 86;
        public const int SM_MEDIACENTER = 87;
        public const int SM_STARTER = 88;
        public const int SM_SERVERR2 = 89;
        ////#endif /* _WIN32_WINNT >= 0x0501 */

        //..WINVER
        ////#if (WINVER < = 0x0500) && (!defined(_WIN32_WINNT) || (_WIN32_WINNT < = 0x0400));
        public const int SM_CMETRICS = 76;
        ////#elif WINVER == 0x500;
        //public const int SM_CMETRICS = 83;
        ////#else
        //public const int SM_CMETRICS = 90;
        ////#endif

        ////#if(WINVER >= 0x0500);
        public const int SM_REMOTESESSION = 0x1000;


        ////#if(_WIN32_WINNT >= 0x0501);
        public const int SM_SHUTTINGDOWN = 0x2000;
        ////#endif /* _WIN32_WINNT >= 0x0501 */

        ////#if(WINVER >= 0x0501);
        public const int SM_REMOTECONTROL = 0x2001;
        ////#endif /* WINVER >= 0x0501 */

        ////#if(WINVER >= 0x0501);
        public const int SM_CARETBLINKINGENABLED = 0x2002;
        ////#endif /* WINVER >= 0x0501 */

        ////#endif /* WINVER >= 0x0500 */


        ////#if(_WIN32_WINNT >= 0x0501);
        public const int PMB_ACTIVE = 0x00000001;


        ////#if(WINVER >= 0x0400);
        /* return codes for WM_MENUCHAR */
        public const int MNC_IGNORE = 0;
        public const int MNC_CLOSE = 1;
        public const int MNC_EXECUTE = 2;
        public const int MNC_SELECT = 3;


        ////#if(WINVER >= 0x0500);

        public const uint MNS_NOCHECK = 0x80000000;
        public const int MNS_MODELESS = 0x40000000;
        public const int MNS_DRAGDROP = 0x20000000;
        public const int MNS_AUTODISMISS = 0x10000000;
        public const int MNS_NOTIFYBYPOS = 0x08000000;
        public const int MNS_CHECKORBMP = 0x04000000;

        public const int MIM_MAXHEIGHT = 0x00000001;
        public const int MIM_BACKGROUND = 0x00000002;
        public const int MIM_HELPID = 0x00000004;
        public const int MIM_MENUDATA = 0x00000008;
        public const int MIM_STYLE = 0x00000010;
        public const uint MIM_APPLYTOSUBMENUS = 0x80000000;


        /*
         * WM_MENUDRAG return values.
         */
        public const int MND_CONTINUE = 0;
        public const int MND_ENDMENU = 1;


        /*
         * MENUGETOBJECTINFO dwFlags values
         */
        public const int MNGOF_TOPGAP = 0x00000001;
        public const int MNGOF_BOTTOMGAP = 0x00000002;

        /*
         * WM_MENUGETOBJECT return values
         */
        public const int MNGO_NOINTERFACE = 0x00000000;
        public const int MNGO_NOERROR = 0x00000001;
        ////#endif /* WINVER >= 0x0500 */

        ////#if(WINVER >= 0x0400);
        public const int MIIM_STATE = 0x00000001;
        public const int MIIM_ID = 0x00000002;
        public const int MIIM_SUBMENU = 0x00000004;
        public const int MIIM_CHECKMARKS = 0x00000008;
        public const int MIIM_TYPE = 0x00000010;
        public const int MIIM_DATA = 0x00000020;
        ////#endif /* WINVER >= 0x0400 */

        ////#if(WINVER >= 0x0500);
        public const int MIIM_STRING = 0x00000040;
        public const int MIIM_BITMAP = 0x00000080;
        public const int MIIM_FTYPE = 0x00000100;

        public const int HBMMENU_CALLBACK = (-1);
        public const int HBMMENU_SYSTEM = (1);
        public const int HBMMENU_MBAR_RESTORE = (2);
        public const int HBMMENU_MBAR_MINIMIZE = (3);
        public const int HBMMENU_MBAR_CLOSE = (5);
        public const int HBMMENU_MBAR_CLOSE_D = (6);
        public const int HBMMENU_MBAR_MINIMIZE_D = (7);
        public const int HBMMENU_POPUP_CLOSE = (8);
        public const int HBMMENU_POPUP_RESTORE = (9);
        public const int HBMMENU_POPUP_MAXIMIZE = (10);
        public const int HBMMENU_POPUP_MINIMIZE = (11);
        ////#endif /* WINVER >= 0x0500 */

        public const int GMDI_USEDISABLED = 0x0001;
        public const int GMDI_GOINTOPOPUPS = 0x0002;

        /*
         * Flags for TrackPopupMenu
         */
        public const int TPM_LEFTBUTTON = 0x0000;
        public const int TPM_RIGHTBUTTON = 0x0002;
        public const int TPM_LEFTALIGN = 0x0000;
        public const int TPM_CENTERALIGN = 0x0004;
        public const int TPM_RIGHTALIGN = 0x0008;
        ////#if(WINVER >= 0x0400);
        public const int TPM_TOPALIGN = 0x0000;
        public const int TPM_VCENTERALIGN = 0x0010;
        public const int TPM_BOTTOMALIGN = 0x0020;

        public const int TPM_HORIZONTAL = 0x0000; /* Horz alignment matters more */
        public const int TPM_VERTICAL = 0x0040; /* Vert alignment matters more */
        public const int TPM_NONOTIFY = 0x0080; /* Don't send any notification msgs */
        public const int TPM_RETURNCMD = 0x0100;
        ////#endif /* WINVER >= 0x0400 */
        ////#if(WINVER >= 0x0500);
        public const int TPM_RECURSE = 0x0001;
        public const int TPM_HORPOSANIMATION = 0x0400;
        public const int TPM_HORNEGANIMATION = 0x0800;
        public const int TPM_VERPOSANIMATION = 0x1000;
        public const int TPM_VERNEGANIMATION = 0x2000;
        ////#if(_WIN32_WINNT >= 0x0500);
        public const int TPM_NOANIMATION = 0x4000;
        ////#endif /* _WIN32_WINNT >= 0x0500 */
        ////#if(_WIN32_WINNT >= 0x0501);
        public const int TPM_LAYOUTRTL = 0x8000;
        ////#endif /* _WIN32_WINNT >= 0x0501 */
        ////#endif /* WINVER >= 0x0500 */


        ////#endif /* !NOMENUS */

        public const int DOF_EXECUTABLE = 0x8001;
        public const int DOF_DOCUMENT = 0x8002;
        public const int DOF_DIRECTORY = 0x8003;
        public const int DOF_MULTIPLE = 0x8004;
        public const int DOF_PROGMAN = 0x0001;
        public const int DOF_SHELLDATA = 0x0002;

        public const int DO_DROPFILE = 0x454C4946;
        public const int DO_PRINTFILE = 0x544E5250;

        /*
         * DrawText() Format Flags
         */
        public const int DT_TOP = 0x00000000;
        public const int DT_LEFT = 0x00000000;
        public const int DT_CENTER = 0x00000001;
        public const int DT_RIGHT = 0x00000002;
        public const int DT_VCENTER = 0x00000004;
        public const int DT_BOTTOM = 0x00000008;
        public const int DT_WORDBREAK = 0x00000010;
        public const int DT_SINGLELINE = 0x00000020;
        public const int DT_EXPANDTABS = 0x00000040;
        public const int DT_TABSTOP = 0x00000080;
        public const int DT_NOCLIP = 0x00000100;
        public const int DT_EXTERNALLEADING = 0x00000200;
        public const int DT_CALCRECT = 0x00000400;
        public const int DT_NOPREFIX = 0x00000800;
        public const int DT_INTERNAL = 0x00001000;

        //#if(WINVER >= 0x0400);
        public const int DT_EDITCONTROL = 0x00002000;
        public const int DT_PATH_ELLIPSIS = 0x00004000;
        public const int DT_END_ELLIPSIS = 0x00008000;
        public const int DT_MODIFYSTRING = 0x00010000;
        public const int DT_RTLREADING = 0x00020000;
        public const int DT_WORD_ELLIPSIS = 0x00040000;
        //#if(WINVER >= 0x0500);
        public const int DT_NOFULLWIDTHCHARBREAK = 0x00080000;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int DT_HIDEPREFIX = 0x00100000;
        public const int DT_PREFIXONLY = 0x00200000;
        //#endif /* _WIN32_WINNT >= 0x0500 */
        //#endif /* WINVER >= 0x0500 */


        //#if(WINVER >= 0x0400);
        /* Monolithic state-drawing routine */
        /* Image type */
        public const int DST_COMPLEX = 0x0000;
        public const int DST_TEXT = 0x0001;
        public const int DST_PREFIXTEXT = 0x0002;
        public const int DST_ICON = 0x0003;
        public const int DST_BITMAP = 0x0004;

        /* State type */
        public const int DSS_NORMAL = 0x0000;
        public const int DSS_UNION = 0x0010; /* Gray string appearance */
        public const int DSS_DISABLED = 0x0020;
        public const int DSS_MONO = 0x0080;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int DSS_HIDEPREFIX = 0x0200;
        public const int DSS_PREFIXONLY = 0x0400;
        //#endif /* _WIN32_WINNT >= 0x0500 */
        public const int DSS_RIGHT = 0x8000;

        public const int LSFW_LOCK = 1;
        public const int LSFW_UNLOCK = 2;


        /*
         * GetDCEx() flags
         */
        public const int DCX_WINDOW = 0x00000001;
        public const int DCX_CACHE = 0x00000002;
        public const int DCX_NORESETATTRS = 0x00000004;
        public const int DCX_CLIPCHILDREN = 0x00000008;
        public const int DCX_CLIPSIBLINGS = 0x00000010;
        public const int DCX_PARENTCLIP = 0x00000020;
        public const int DCX_EXCLUDERGN = 0x00000040;
        public const int DCX_INTERSECTRGN = 0x00000080;
        public const int DCX_EXCLUDEUPDATE = 0x00000100;
        public const int DCX_INTERSECTUPDATE = 0x00000200;
        public const int DCX_LOCKWINDOWUPDATE = 0x00000400;

        public const int DCX_VALIDATE = 0x00200000;

        /*
         * RedrawWindow() flags
         */
        public const int RDW_INVALIDATE = 0x0001;
        public const int RDW_INTERNALPAINT = 0x0002;
        public const int RDW_ERASE = 0x0004;

        public const int RDW_VALIDATE = 0x0008;
        public const int RDW_NOINTERNALPAINT = 0x0010;
        public const int RDW_NOERASE = 0x0020;

        public const int RDW_NOCHILDREN = 0x0040;
        public const int RDW_ALLCHILDREN = 0x0080;

        public const int RDW_UPDATENOW = 0x0100;
        public const int RDW_ERASENOW = 0x0200;

        public const int RDW_FRAME = 0x0400;
        public const int RDW_NOFRAME = 0x0800;

        public const int SW_SCROLLCHILDREN = 0x0001; /* Scroll children within *lprcScroll. */
        public const int SW_INVALIDATE = 0x0002; /* Invalidate after scrolling */
        public const int SW_ERASE = 0x0004; /* If SW_INVALIDATE, don't send WM_ERASEBACKGROUND */
        //#if(WINVER >= 0x0500);
        public const int SW_SMOOTHSCROLL = 0x0010; /* Use smooth scrolling */
        //#endif /* WINVER >= 0x0500 */

        /*
         * EnableScrollBar() flags
         */
        public const int ESB_ENABLE_BOTH = 0x0000;
        public const int ESB_DISABLE_BOTH = 0x0003;

        public const int ESB_DISABLE_LEFT = 0x0001;
        public const int ESB_DISABLE_RIGHT = 0x0002;

        public const int ESB_DISABLE_UP = 0x0001;
        public const int ESB_DISABLE_DOWN = 0x0002;


        public const int ESB_DISABLE_LTUP = ESB_DISABLE_LEFT;
        public const int ESB_DISABLE_RTDN = ESB_DISABLE_RIGHT;

        public const int HELPINFO_WINDOW = 0x0001;
        public const int HELPINFO_MENUITEM = 0x0002;

        /*
         * MessageBox() Flags
         */
        public const int MB_OK = 0x00000000;
        public const int MB_OKCANCEL = 0x00000001;
        public const int MB_ABORTRETRYIGNORE = 0x00000002;
        public const int MB_YESNOCANCEL = 0x00000003;
        public const int MB_YESNO = 0x00000004;
        public const int MB_RETRYCANCEL = 0x00000005;
        //#if(WINVER >= 0x0500);
        public const int MB_CANCELTRYCONTINUE = 0x00000006;
        //#endif /* WINVER >= 0x0500 */


        public const int MB_ICONHAND = 0x00000010;
        public const int MB_ICONQUESTION = 0x00000020;
        public const int MB_ICONEXCLAMATION = 0x00000030;
        public const int MB_ICONASTERISK = 0x00000040;

        //#if(WINVER >= 0x0400);
        public const int MB_USERICON = 0x00000080;
        public const int MB_ICONWARNING = MB_ICONEXCLAMATION;
        public const int MB_ICONERROR = MB_ICONHAND;
        //#endif /* WINVER >= 0x0400 */

        public const int MB_ICONINFORMATION = MB_ICONASTERISK;
        public const int MB_ICONSTOP = MB_ICONHAND;

        public const int MB_DEFBUTTON1 = 0x00000000;
        public const int MB_DEFBUTTON2 = 0x00000100;
        public const int MB_DEFBUTTON3 = 0x00000200;
        //#if(WINVER >= 0x0400);
        public const int MB_DEFBUTTON4 = 0x00000300;
        //#endif /* WINVER >= 0x0400 */

        public const int MB_APPLMODAL = 0x00000000;
        public const int MB_SYSTEMMODAL = 0x00001000;
        public const int MB_TASKMODAL = 0x00002000;
        //#if(WINVER >= 0x0400);
        public const int MB_HELP = 0x00004000; // Help Button
        //#endif /* WINVER >= 0x0400 */

        public const int MB_NOFOCUS = 0x00008000;
        public const int MB_SETFOREGROUND = 0x00010000;
        public const int MB_DEFAULT_DESKTOP_ONLY = 0x00020000;

        //#if(WINVER >= 0x0400);
        public const int MB_TOPMOST = 0x00040000;
        public const int MB_RIGHT = 0x00080000;
        public const int MB_RTLREADING = 0x00100000;

        //#endif /* WINVER >= 0x0400 */

        public const int MB_SERVICE_NOTIFICATION_NT3X = 0x00040000;
        //#endif

        public const int MB_TYPEMASK = 0x0000000F;
        public const int MB_ICONMASK = 0x000000F0;
        public const int MB_DEFMASK = 0x00000F00;
        public const int MB_MODEMASK = 0x00003000;
        public const int MB_MISCMASK = 0x0000C000;

        public const int CWP_ALL = 0x0000;
        public const int CWP_SKIPINVISIBLE = 0x0001;
        public const int CWP_SKIPDISABLED = 0x0002;
        public const int CWP_SKIPTRANSPARENT = 0x0004;

        /*
         * Color Types
         */
        public const int CTLCOLOR_MSGBOX = 0;
        public const int CTLCOLOR_EDIT = 1;
        public const int CTLCOLOR_LISTBOX = 2;
        public const int CTLCOLOR_BTN = 3;
        public const int CTLCOLOR_DLG = 4;
        public const int CTLCOLOR_SCROLLBAR = 5;
        public const int CTLCOLOR_STATIC = 6;
        public const int CTLCOLOR_MAX = 7;

        public const int COLOR_SCROLLBAR = 0;
        public const int COLOR_BACKGROUND = 1;
        public const int COLOR_ACTIVECAPTION = 2;
        public const int COLOR_INACTIVECAPTION = 3;
        public const int COLOR_MENU = 4;
        public const int COLOR_WINDOW = 5;
        public const int COLOR_WINDOWFRAME = 6;
        public const int COLOR_MENUTEXT = 7;
        public const int COLOR_WINDOWTEXT = 8;
        public const int COLOR_CAPTIONTEXT = 9;
        public const int COLOR_ACTIVEBORDER = 10;
        public const int COLOR_INACTIVEBORDER = 11;
        public const int COLOR_APPWORKSPACE = 12;
        public const int COLOR_HIGHLIGHT = 13;
        public const int COLOR_HIGHLIGHTTEXT = 14;
        public const int COLOR_BTNFACE = 15;
        public const int COLOR_BTNSHADOW = 16;
        public const int COLOR_GRAYTEXT = 17;
        public const int COLOR_BTNTEXT = 18;
        public const int COLOR_INACTIVECAPTIONTEXT = 19;
        public const int COLOR_BTNHIGHLIGHT = 20;

        //#if(WINVER >= 0x0400);
        public const int COLOR_3DDKSHADOW = 21;
        public const int COLOR_3DLIGHT = 22;
        public const int COLOR_INFOTEXT = 23;
        public const int COLOR_INFOBK = 24;
        //#endif /* WINVER >= 0x0400 */

        //#if(WINVER >= 0x0500);
        public const int COLOR_HOTLIGHT = 26;
        public const int COLOR_GRADIENTACTIVECAPTION = 27;
        public const int COLOR_GRADIENTINACTIVECAPTION = 28;
        //#if(WINVER >= 0x0501);
        public const int COLOR_MENUHILIGHT = 29;
        public const int COLOR_MENUBAR = 30;
        //#endif /* WINVER >= 0x0501 */
        //#endif /* WINVER >= 0x0500 */

        //#if(WINVER >= 0x0400);
        public const int COLOR_DESKTOP = COLOR_BACKGROUND;
        public const int COLOR_3DFACE = COLOR_BTNFACE;
        public const int COLOR_3DSHADOW = COLOR_BTNSHADOW;
        public const int COLOR_3DHIGHLIGHT = COLOR_BTNHIGHLIGHT;
        public const int COLOR_3DHILIGHT = COLOR_BTNHIGHLIGHT;
        public const int COLOR_BTNHILIGHT = COLOR_BTNHIGHLIGHT;
        //#endif /* WINVER >= 0x0400 */

        /*
         * GetWindow() Constants
         */
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        //..WINVER
        //#if(WINVER <= 0x0400);
        //public const int GW_MAX = 5;
        //#else
        public const int GW_ENABLEDPOPUP = 6;
        public const int GW_MAX = 6;
        //#endif

        /* ;win40 = -- A lot of MF_* flags have been renamed as MFT_* and MFS_* flags */
        /*
         * Menu flags for Add/Check/EnableMenuItem()
         */
        public const int MF_INSERT = 0x00000000;
        public const int MF_CHANGE = 0x00000080;
        public const int MF_APPEND = 0x00000100;
        public const int MF_DELETE = 0x00000200;
        public const int MF_REMOVE = 0x00001000;

        public const int MF_BYCOMMAND = 0x00000000;
        public const int MF_BYPOSITION = 0x00000400;

        public const int MF_SEPARATOR = 0x00000800;

        public const int MF_ENABLED = 0x00000000;
        public const int MF_GRAYED = 0x00000001;
        public const int MF_DISABLED = 0x00000002;

        public const int MF_UNCHECKED = 0x00000000;
        public const int MF_CHECKED = 0x00000008;
        public const int MF_USECHECKBITMAPS = 0x00000200;

        public const int MF_STRING = 0x00000000;
        public const int MF_BITMAP = 0x00000004;
        public const int MF_OWNERDRAW = 0x00000100;

        public const int MF_POPUP = 0x00000010;
        public const int MF_MENUBARBREAK = 0x00000020;
        public const int MF_MENUBREAK = 0x00000040;

        public const int MF_UNHILITE = 0x00000000;
        public const int MF_HILITE = 0x00000080;

        //#if(WINVER >= 0x0400);
        public const int MF_DEFAULT = 0x00001000;
        //#endif /* WINVER >= 0x0400 */
        public const int MF_SYSMENU = 0x00002000;
        public const int MF_HELP = 0x00004000;
        //#if(WINVER >= 0x0400);
        public const int MF_RIGHTJUSTIFY = 0x00004000;
        //#endif /* WINVER >= 0x0400 */

        public const int MF_MOUSESELECT = 0x00008000;
        //#if(WINVER >= 0x0400);
        public const int MF_END = 0x00000080; /* Obsolete -- only used by old RES files */
        //#endif /* WINVER >= 0x0400 */


        //#if(WINVER >= 0x0400);
        public const int MFT_STRING = MF_STRING;
        public const int MFT_BITMAP = MF_BITMAP;
        public const int MFT_MENUBARBREAK = MF_MENUBARBREAK;
        public const int MFT_MENUBREAK = MF_MENUBREAK;
        public const int MFT_OWNERDRAW = MF_OWNERDRAW;
        public const int MFT_RADIOCHECK = 0x00000200;
        public const int MFT_SEPARATOR = MF_SEPARATOR;
        public const int MFT_RIGHTORDER = 0x00002000;
        public const int MFT_RIGHTJUSTIFY = MF_RIGHTJUSTIFY;

        /* Menu flags for Add/Check/EnableMenuItem() */
        public const int MFS_GRAYED = 0x00000003;
        public const int MFS_DISABLED = MFS_GRAYED;
        public const int MFS_CHECKED = MF_CHECKED;
        public const int MFS_HILITE = MF_HILITE;
        public const int MFS_ENABLED = MF_ENABLED;
        public const int MFS_UNCHECKED = MF_UNCHECKED;
        public const int MFS_UNHILITE = MF_UNHILITE;
        public const int MFS_DEFAULT = MF_DEFAULT;
        //#endif /* WINVER >= 0x0400 */


        /*
         * System Menu Command Values
         */
        public const int SC_SIZE = 0xF000;
        public const int SC_MOVE = 0xF010;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_NEXTWINDOW = 0xF040;
        public const int SC_PREVWINDOW = 0xF050;
        public const int SC_CLOSE = 0xF060;
        public const int SC_VSCROLL = 0xF070;
        public const int SC_HSCROLL = 0xF080;
        public const int SC_MOUSEMENU = 0xF090;
        public const int SC_KEYMENU = 0xF100;
        public const int SC_ARRANGE = 0xF110;
        public const int SC_RESTORE = 0xF120;
        public const int SC_TASKLIST = 0xF130;
        public const int SC_SCREENSAVE = 0xF140;
        public const int SC_HOTKEY = 0xF150;
        //#if(WINVER >= 0x0400);
        public const int SC_DEFAULT = 0xF160;
        public const int SC_MONITORPOWER = 0xF170;
        public const int SC_CONTEXTHELP = 0xF180;
        public const int SC_SEPARATOR = 0xF00F;
        //#endif /* WINVER >= 0x0400 */


        /*
         * Obsolete names
         */
        public const int SC_ICON = SC_MINIMIZE;
        public const int SC_ZOOM = SC_MAXIMIZE;




        public const int IMAGE_BITMAP = 0;
        public const int IMAGE_ICON = 1;
        public const int IMAGE_CURSOR = 2;
        //#if(WINVER >= 0x0400);
        public const int IMAGE_ENHMETAFILE = 3;

        public const int LR_DEFAULTCOLOR = 0x0000;
        public const int LR_MONOCHROME = 0x0001;
        public const int LR_COLOR = 0x0002;
        public const int LR_COPYRETURNORG = 0x0004;
        public const int LR_COPYDELETEORG = 0x0008;
        public const int LR_LOADFROMFILE = 0x0010;
        public const int LR_LOADTRANSPARENT = 0x0020;
        public const int LR_DEFAULTSIZE = 0x0040;
        public const int LR_VGACOLOR = 0x0080;
        public const int LR_LOADMAP3DCOLORS = 0x1000;
        public const int LR_CREATEDIBSECTION = 0x2000;
        public const int LR_COPYFROMRESOURCE = 0x4000;
        public const int LR_SHARED = 0x8000;


        public const int DI_MASK = 0x0001;
        public const int DI_IMAGE = 0x0002;
        public const int DI_NORMAL = 0x0003;
        public const int DI_COMPAT = 0x0004;
        public const int DI_DEFAULTSIZE = 0x0008;
        //#if(_WIN32_WINNT >= 0x0501);
        public const int DI_NOMIRROR = 0x0010;
        //#endif /* _WIN32_WINNT >= 0x0501 */


        //#if(WINVER >= 0x0400);
        public const int RES_ICON = 1;
        public const int RES_CURSOR = 2;
        //#endif /* WINVER >= 0x0400 */


        /*
         * OEM Resource Ordinal Numbers
         */
        public const int OBM_CLOSE = 32754;
        public const int OBM_UPARROW = 32753;
        public const int OBM_DNARROW = 32752;
        public const int OBM_RGARROW = 32751;
        public const int OBM_LFARROW = 32750;
        public const int OBM_REDUCE = 32749;
        public const int OBM_ZOOM = 32748;
        public const int OBM_RESTORE = 32747;
        public const int OBM_REDUCED = 32746;
        public const int OBM_ZOOMD = 32745;
        public const int OBM_RESTORED = 32744;
        public const int OBM_UPARROWD = 32743;
        public const int OBM_DNARROWD = 32742;
        public const int OBM_RGARROWD = 32741;
        public const int OBM_LFARROWD = 32740;
        public const int OBM_MNARROW = 32739;
        public const int OBM_COMBO = 32738;
        public const int OBM_UPARROWI = 32737;
        public const int OBM_DNARROWI = 32736;
        public const int OBM_RGARROWI = 32735;
        public const int OBM_LFARROWI = 32734;

        public const int OBM_OLD_CLOSE = 32767;
        public const int OBM_SIZE = 32766;
        public const int OBM_OLD_UPARROW = 32765;
        public const int OBM_OLD_DNARROW = 32764;
        public const int OBM_OLD_RGARROW = 32763;
        public const int OBM_OLD_LFARROW = 32762;
        public const int OBM_BTSIZE = 32761;
        public const int OBM_CHECK = 32760;
        public const int OBM_CHECKBOXES = 32759;
        public const int OBM_BTNCORNERS = 32758;
        public const int OBM_OLD_REDUCE = 32757;
        public const int OBM_OLD_ZOOM = 32756;
        public const int OBM_OLD_RESTORE = 32755;


        public const int OCR_NORMAL = 32512;
        public const int OCR_IBEAM = 32513;
        public const int OCR_WAIT = 32514;
        public const int OCR_CROSS = 32515;
        public const int OCR_UP = 32516;
        public const int OCR_SIZE = 32640; /* OBSOLETE: use OCR_SIZEALL */
        public const int OCR_ICON = 32641; /* OBSOLETE: use OCR_NORMAL */
        public const int OCR_SIZENWSE = 32642;
        public const int OCR_SIZENESW = 32643;
        public const int OCR_SIZEWE = 32644;
        public const int OCR_SIZENS = 32645;
        public const int OCR_SIZEALL = 32646;
        public const int OCR_ICOCUR = 32647; /* OBSOLETE: use OIC_WINLOGO */
        public const int OCR_NO = 32648;
        //#if(WINVER >= 0x0500);
        public const int OCR_HAND = 32649;
        //#endif /* WINVER >= 0x0500 */
        //#if(WINVER >= 0x0400);
        public const int OCR_APPSTARTING = 32650;
        //#endif /* WINVER >= 0x0400 */

        public const int OIC_SAMPLE = 32512;
        public const int OIC_HAND = 32513;
        public const int OIC_QUES = 32514;
        public const int OIC_BANG = 32515;
        public const int OIC_NOTE = 32516;
        //#if(WINVER >= 0x0400);
        public const int OIC_WINLOGO = 32517;
        public const int OIC_WARNING = OIC_BANG;
        public const int OIC_ERROR = OIC_HAND;
        public const int OIC_INFORMATION = OIC_NOTE;
        //#endif /* WINVER >= 0x0400 */


        public const int ORD_LANGDRIVER = 1; /* The ordinal number for the entry point of
 = ** language drivers.
 = */


        /*
         * Standard Icon IDs
         */
        /* WINVER >= 0x0400 */


        /*
         * Dialog Box Command IDs
         */
        public const int IDOK = 1;
        public const int IDCANCEL = 2;
        public const int IDABORT = 3;
        public const int IDRETRY = 4;
        public const int IDIGNORE = 5;
        public const int IDYES = 6;
        public const int IDNO = 7;
        //#if(WINVER >= 0x0400);
        public const int IDCLOSE = 8;
        public const int IDHELP = 9;
        //#endif /* WINVER >= 0x0400 */

        //#if(WINVER >= 0x0500);
        public const int IDTRYAGAIN = 10;
        public const int IDCONTINUE = 11;
        //#endif /* WINVER >= 0x0500 */

        //#if(WINVER >= 0x0501);
        //#ifndef IDTIMEOUT
        public const int IDTIMEOUT = 32000;
        //#endif
        //#endif /* WINVER >= 0x0501 */


        /*
         * Edit Control Styles
         */
        public const int ES_LEFT = 0x0000;
        public const int ES_CENTER = 0x0001;
        public const int ES_RIGHT = 0x0002;
        public const int ES_MULTILINE = 0x0004;
        public const int ES_UPPERCASE = 0x0008;
        public const int ES_LOWERCASE = 0x0010;
        public const int ES_PASSWORD = 0x0020;
        public const int ES_AUTOVSCROLL = 0x0040;
        public const int ES_AUTOHSCROLL = 0x0080;
        public const int ES_NOHIDESEL = 0x0100;
        public const int ES_OEMCONVERT = 0x0400;
        public const int ES_READONLY = 0x0800;
        public const int ES_WANTRETURN = 0x1000;
        //#if(WINVER >= 0x0400);
        public const int ES_NUMBER = 0x2000;
        //#endif; /* WINVER >= 0x0400 */


        //#endif; /* !NOWINSTYLES */

        /*
         * Edit Control Notification Codes
         */
        public const int EN_SETFOCUS = 0x0100;
        public const int EN_KILLFOCUS = 0x0200;
        public const int EN_CHANGE = 0x0300;
        public const int EN_UPDATE = 0x0400;
        public const int EN_ERRSPACE = 0x0500;
        public const int EN_MAXTEXT = 0x0501;
        public const int EN_HSCROLL = 0x0601;
        public const int EN_VSCROLL = 0x0602;

        //#if(_WIN32_WINNT >= 0x0500);
        public const int EN_ALIGN_LTR_EC = 0x0700;
        public const int EN_ALIGN_RTL_EC = 0x0701;
        //#endif; /* _WIN32_WINNT >= 0x0500 */

        //#if(WINVER >= 0x0400);
        /* Edit control EM_SETMARGIN parameters */
        public const int EC_LEFTMARGIN = 0x0001;
        public const int EC_RIGHTMARGIN = 0x0002;
        public const int EC_USEFONTINFO = 0xffff;
        //#endif; /* WINVER >= 0x0400 */

        //#if(WINVER >= 0x0500);
        /* wParam of EM_GET/SETIMESTATUS = */
        public const int EMSIS_COMPOSITIONSTRING = 0x0001;

        /* lParam for EMSIS_COMPOSITIONSTRING = */
        public const int EIMES_GETCOMPSTRATONCE = 0x0001;
        public const int EIMES_CANCELCOMPSTRINFOCUS = 0x0002;
        public const int EIMES_COMPLETECOMPSTRKILLFOCUS = 0x0004;
        //#endif; /* WINVER >= 0x0500 */

        //#ifndef NOWINMESSAGES


        /*
         * Edit Control Messages
         */
        public const int EM_GETSEL = 0x00B0;
        public const int EM_SETSEL = 0x00B1;
        public const int EM_GETRECT = 0x00B2;
        public const int EM_SETRECT = 0x00B3;
        public const int EM_SETRECTNP = 0x00B4;
        public const int EM_SCROLL = 0x00B5;
        public const int EM_LINESCROLL = 0x00B6;
        public const int EM_SCROLLCARET = 0x00B7;
        public const int EM_GETMODIFY = 0x00B8;
        public const int EM_SETMODIFY = 0x00B9;
        public const int EM_GETLINECOUNT = 0x00BA;
        public const int EM_LINEINDEX = 0x00BB;
        public const int EM_SETHANDLE = 0x00BC;
        public const int EM_GETHANDLE = 0x00BD;
        public const int EM_GETTHUMB = 0x00BE;
        public const int EM_LINELENGTH = 0x00C1;
        public const int EM_REPLACESEL = 0x00C2;
        public const int EM_GETLINE = 0x00C4;
        public const int EM_LIMITTEXT = 0x00C5;
        public const int EM_CANUNDO = 0x00C6;
        public const int EM_UNDO = 0x00C7;
        public const int EM_FMTLINES = 0x00C8;
        public const int EM_LINEFROMCHAR = 0x00C9;
        public const int EM_SETTABSTOPS = 0x00CB;
        public const int EM_SETPASSWORDCHAR = 0x00CC;
        public const int EM_EMPTYUNDOBUFFER = 0x00CD;
        public const int EM_GETFIRSTVISIBLELINE = 0x00CE;
        public const int EM_SETREADONLY = 0x00CF;
        public const int EM_SETWORDBREAKPROC = 0x00D0;
        public const int EM_GETWORDBREAKPROC = 0x00D1;
        public const int EM_GETPASSWORDCHAR = 0x00D2;
        //#if(WINVER >= 0x0400);
        public const int EM_SETMARGINS = 0x00D3;
        public const int EM_GETMARGINS = 0x00D4;
        public const int EM_SETLIMITTEXT = EM_LIMITTEXT; /* ;win40 Name change */
        public const int EM_GETLIMITTEXT = 0x00D5;
        public const int EM_POSFROMCHAR = 0x00D6;
        public const int EM_CHARFROMPOS = 0x00D7;
        //#endif; /* WINVER >= 0x0400 */

        //#if(WINVER >= 0x0500);
        public const int EM_SETIMESTATUS = 0x00D8;
        public const int EM_GETIMESTATUS = 0x00D9;
        //#endif; /* WINVER >= 0x0500 */


        //#endif; /* !NOWINMESSAGES */

        /*
         * EDITWORDBREAKPROC code values
         */
        public const int WB_LEFT = 0;
        public const int WB_RIGHT = 1;
        public const int WB_ISDELIMITER = 2;


        /*
         * Button Control Styles
         */
        public const int BS_PUSHBUTTON = 0x00000000;
        public const int BS_DEFPUSHBUTTON = 0x00000001;
        public const int BS_CHECKBOX = 0x00000002;
        public const int BS_AUTOCHECKBOX = 0x00000003;
        public const int BS_RADIOBUTTON = 0x00000004;
        public const int BS_3STATE = 0x00000005;
        public const int BS_AUTO3STATE = 0x00000006;
        public const int BS_GROUPBOX = 0x00000007;
        public const int BS_USERBUTTON = 0x00000008;
        public const int BS_AUTORADIOBUTTON = 0x00000009;
        public const int BS_PUSHBOX = 0x0000000A;
        public const int BS_OWNERDRAW = 0x0000000B;
        public const int BS_TYPEMASK = 0x0000000F;
        public const int BS_LEFTTEXT = 0x00000020;
        //#if(WINVER >= 0x0400);
        public const int BS_TEXT = 0x00000000;
        public const int BS_ICON = 0x00000040;
        public const int BS_BITMAP = 0x00000080;
        public const int BS_LEFT = 0x00000100;
        public const int BS_RIGHT = 0x00000200;
        public const int BS_CENTER = 0x00000300;
        public const int BS_TOP = 0x00000400;
        public const int BS_BOTTOM = 0x00000800;
        public const int BS_VCENTER = 0x00000C00;
        public const int BS_PUSHLIKE = 0x00001000;
        public const int BS_MULTILINE = 0x00002000;
        public const int BS_NOTIFY = 0x00004000;
        public const int BS_FLAT = 0x00008000;
        public const int BS_RIGHTBUTTON = BS_LEFTTEXT;
        //#endif; /* WINVER >= 0x0400 */

        /*
         * User Button Notification Codes
         */
        public const int BN_CLICKED = 0;
        public const int BN_PAINT = 1;
        public const int BN_HILITE = 2;
        public const int BN_UNHILITE = 3;
        public const int BN_DISABLE = 4;
        public const int BN_DOUBLECLICKED = 5;
        //#if(WINVER >= 0x0400);
        public const int BN_PUSHED = BN_HILITE;
        public const int BN_UNPUSHED = BN_UNHILITE;
        public const int BN_DBLCLK = BN_DOUBLECLICKED;
        public const int BN_SETFOCUS = 6;
        public const int BN_KILLFOCUS = 7;
        //#endif; /* WINVER >= 0x0400 */

        /*
         * Button Control Messages
         */
        public const int BM_GETCHECK = 0x00F0;
        public const int BM_SETCHECK = 0x00F1;
        public const int BM_GETSTATE = 0x00F2;
        public const int BM_SETSTATE = 0x00F3;
        public const int BM_SETSTYLE = 0x00F4;
        //#if(WINVER >= 0x0400);
        public const int BM_CLICK = 0x00F5;
        public const int BM_GETIMAGE = 0x00F6;
        public const int BM_SETIMAGE = 0x00F7;

        public const int BST_UNCHECKED = 0x0000;
        public const int BST_CHECKED = 0x0001;
        public const int BST_INDETERMINATE = 0x0002;
        public const int BST_PUSHED = 0x0004;
        public const int BST_FOCUS = 0x0008;
        //#endif; /* WINVER >= 0x0400 */

        /*
         * Static Control Constants
         */
        public const int SS_LEFT = 0x00000000;
        public const int SS_CENTER = 0x00000001;
        public const int SS_RIGHT = 0x00000002;
        public const int SS_ICON = 0x00000003;
        public const int SS_BLACKRECT = 0x00000004;
        public const int SS_GRAYRECT = 0x00000005;
        public const int SS_WHITERECT = 0x00000006;
        public const int SS_BLACKFRAME = 0x00000007;
        public const int SS_GRAYFRAME = 0x00000008;
        public const int SS_WHITEFRAME = 0x00000009;
        public const int SS_USERITEM = 0x0000000A;
        public const int SS_SIMPLE = 0x0000000B;
        public const int SS_LEFTNOWORDWRAP = 0x0000000C;
        //#if(WINVER >= 0x0400);
        public const int SS_OWNERDRAW = 0x0000000D;
        public const int SS_BITMAP = 0x0000000E;
        public const int SS_ENHMETAFILE = 0x0000000F;
        public const int SS_ETCHEDHORZ = 0x00000010;
        public const int SS_ETCHEDVERT = 0x00000011;
        public const int SS_ETCHEDFRAME = 0x00000012;
        public const int SS_TYPEMASK = 0x0000001F;
        //#endif; /* WINVER >= 0x0400 */
        //#if(WINVER >= 0x0501);
        public const int SS_REALSIZECONTROL = 0x00000040;
        //#endif; /* WINVER >= 0x0501 */
        public const int SS_NOPREFIX = 0x00000080; /* Don't do "&" character translation */
        //#if(WINVER >= 0x0400);
        public const int SS_NOTIFY = 0x00000100;
        public const int SS_CENTERIMAGE = 0x00000200;
        public const int SS_RIGHTJUST = 0x00000400;
        public const int SS_REALSIZEIMAGE = 0x00000800;
        public const int SS_SUNKEN = 0x00001000;
        public const int SS_EDITCONTROL = 0x00002000;
        public const int SS_ENDELLIPSIS = 0x00004000;
        public const int SS_PATHELLIPSIS = 0x00008000;
        public const int SS_WORDELLIPSIS = 0x0000C000;
        public const int SS_ELLIPSISMASK = 0x0000C000;
        //#endif; /* WINVER >= 0x0400 */


        /*
         * Static Control Mesages
         */
        public const int STM_SETICON = 0x0170;
        public const int STM_GETICON = 0x0171;
        //#if(WINVER >= 0x0400);
        public const int STM_SETIMAGE = 0x0172;
        public const int STM_GETIMAGE = 0x0173;
        public const int STN_CLICKED = 0;
        public const int STN_DBLCLK = 1;
        public const int STN_ENABLE = 2;
        public const int STN_DISABLE = 3;
        //#endif; /* WINVER >= 0x0400 */
        public const int STM_MSGMAX = 0x0174;
        //#endif; /* !NOWINMESSAGES */


        /*
         * Get/SetWindowWord/Long offsets for use with WC_DIALOG windows
         */
        public const int DWL_MSGRESULT = 0;
        public const int DWL_DLGPROC = 4;
        public const int DWL_USER = 8;


        public const int DWLP_MSGRESULT = 0;
        //public const int DWLP_DLGPROC = DWLP_MSGRESULT + sizeof(LRESULT)
        //public const int DWLP_USER = DWLP_DLGPROC + sizeof(DLGPROC)


        /*
         * DlgDirList, DlgDirListComboBox flags values
         */
        public const int DDL_READWRITE = 0x0000;
        public const int DDL_READONLY = 0x0001;
        public const int DDL_HIDDEN = 0x0002;
        public const int DDL_SYSTEM = 0x0004;
        public const int DDL_DIRECTORY = 0x0010;
        public const int DDL_ARCHIVE = 0x0020;

        public const int DDL_POSTMSGS = 0x2000;
        public const int DDL_DRIVES = 0x4000;
        public const int DDL_EXCLUSIVE = 0x8000;

        /*
         * Dialog Styles
         */
        public const int DS_ABSALIGN = 0x01;
        public const int DS_SYSMODAL = 0x02;
        public const int DS_LOCALEDIT = 0x20; /* Edit items get Local storage. */
        public const int DS_SETFONT = 0x40; /* User specified font for Dlg controls */
        public const int DS_MODALFRAME = 0x80; /* Can be combined with WS_CAPTION = */
        public const int DS_NOIDLEMSG = 0x100; /* WM_ENTERIDLE message will not be sent */
        public const int DS_SETFOREGROUND = 0x200; /* not in win3.1 */


        //#if(WINVER >= 0x0400);
        public const int DS_3DLOOK = 0x0004;
        public const int DS_FIXEDSYS = 0x0008;
        public const int DS_NOFAILCREATE = 0x0010;
        public const int DS_CONTROL = 0x0400;
        public const int DS_CENTER = 0x0800;
        public const int DS_CENTERMOUSE = 0x1000;
        public const int DS_CONTEXTHELP = 0x2000;

        public const int DS_SHELLFONT = (DS_SETFONT | DS_FIXEDSYS);
        //#endif; /* WINVER >= 0x0400 */

        //#if(_WIN32_WCE >= 0x0500);
        public const int DS_USEPIXELS = 0x8000;
        //#endif

        /*
         * Returned in HIWORD() of DM_GETDEFID result if msg is supported
         */
        public const int DC_HASDEFID = 0x534B;

        /*
         * Dialog Codes
         */
        public const int DLGC_WANTARROWS = 0x0001; /* Control wants arrow keys = */
        public const int DLGC_WANTTAB = 0x0002; /* Control wants tab keys = */
        public const int DLGC_WANTALLKEYS = 0x0004; /* Control wants all keys = */
        public const int DLGC_WANTMESSAGE = 0x0004; /* Pass message to control = */
        public const int DLGC_HASSETSEL = 0x0008; /* Understands EM_SETSEL message = */
        public const int DLGC_DEFPUSHBUTTON = 0x0010; /* Default pushbutton = */
        public const int DLGC_UNDEFPUSHBUTTON = 0x0020; /* Non-default pushbutton = */
        public const int DLGC_RADIOBUTTON = 0x0040; /* Radio button = */
        public const int DLGC_WANTCHARS = 0x0080; /* Want WM_CHAR messages = */
        public const int DLGC_STATIC = 0x0100; /* Static item: don't include = */
        public const int DLGC_BUTTON = 0x2000; /* Button item: can be checked = */

        public const int LB_CTLCODE = 0;

        /*
         * Listbox Return Values
         */
        public const int LB_OKAY = 0;
        public const int LB_ERR = (-1);
        public const int LB_ERRSPACE = (-2);

        /*
        ** = The idStaticPath parameter to DlgDirList can have the following values;
        ** = ORed if the list box should show other details of the files along with;
        ** = the name of the files;
        */
        /* all other details also will be returned */


        /*
         * Listbox Notification Codes
         */
        public const int LBN_ERRSPACE = (-2);
        public const int LBN_SELCHANGE = 1;
        public const int LBN_DBLCLK = 2;
        public const int LBN_SELCANCEL = 3;
        public const int LBN_SETFOCUS = 4;
        public const int LBN_KILLFOCUS = 5;

        //#ifndef NOWINMESSAGES

        /*
         * Listbox messages
         */
        public const int LB_ADDSTRING = 0x0180;
        public const int LB_INSERTSTRING = 0x0181;
        public const int LB_DELETESTRING = 0x0182;
        public const int LB_SELITEMRANGEEX = 0x0183;
        public const int LB_RESETCONTENT = 0x0184;
        public const int LB_SETSEL = 0x0185;
        public const int LB_SETCURSEL = 0x0186;
        public const int LB_GETSEL = 0x0187;
        public const int LB_GETCURSEL = 0x0188;
        public const int LB_GETTEXT = 0x0189;
        public const int LB_GETTEXTLEN = 0x018A;
        public const int LB_GETCOUNT = 0x018B;
        public const int LB_SELECTSTRING = 0x018C;
        public const int LB_DIR = 0x018D;
        public const int LB_GETTOPINDEX = 0x018E;
        public const int LB_FINDSTRING = 0x018F;
        public const int LB_GETSELCOUNT = 0x0190;
        public const int LB_GETSELITEMS = 0x0191;
        public const int LB_SETTABSTOPS = 0x0192;
        public const int LB_GETHORIZONTALEXTENT = 0x0193;
        public const int LB_SETHORIZONTALEXTENT = 0x0194;
        public const int LB_SETCOLUMNWIDTH = 0x0195;
        public const int LB_ADDFILE = 0x0196;
        public const int LB_SETTOPINDEX = 0x0197;
        public const int LB_GETITEMRECT = 0x0198;
        public const int LB_GETITEMDATA = 0x0199;
        public const int LB_SETITEMDATA = 0x019A;
        public const int LB_SELITEMRANGE = 0x019B;
        public const int LB_SETANCHORINDEX = 0x019C;
        public const int LB_GETANCHORINDEX = 0x019D;
        public const int LB_SETCARETINDEX = 0x019E;
        public const int LB_GETCARETINDEX = 0x019F;
        public const int LB_SETITEMHEIGHT = 0x01A0;
        public const int LB_GETITEMHEIGHT = 0x01A1;
        public const int LB_FINDSTRINGEXACT = 0x01A2;
        public const int LB_SETLOCALE = 0x01A5;
        public const int LB_GETLOCALE = 0x01A6;
        public const int LB_SETCOUNT = 0x01A7;
        //#if(WINVER >= 0x0400);
        public const int LB_INITSTORAGE = 0x01A8;
        public const int LB_ITEMFROMPOINT = 0x01A9;
        //#endif; /* WINVER >= 0x0400 */
        //#if(_WIN32_WCE >= 0x0400);
        public const int LB_MULTIPLEADDSTRING = 0x01B1;
        //#endif


        //#if(_WIN32_WINNT >= 0x0501);
        public const int LB_GETLISTBOXINFO = 0x01B2;
        //#endif; /* _WIN32_WINNT >= 0x0501 */

        //..WINVER
        //#if(_WIN32_WINNT >= 0x0501);
        public const int LB_MSGMAX = 0x01B3;
        //#elif(_WIN32_WCE >= 0x0400);
        //public const int LB_MSGMAX = 0x01B1;
        //#elif(WINVER >= 0x0400);
        //public const int LB_MSGMAX = 0x01B0;
        ////#else
        //public const int LB_MSGMAX = 0x01A8;
        //#endif

        //#endif; /* !NOWINMESSAGES */

        //#ifndef NOWINSTYLES


        /*
         * Listbox Styles
         */
        public const int LBS_NOTIFY = 0x0001;
        public const int LBS_SORT = 0x0002;
        public const int LBS_NOREDRAW = 0x0004;
        public const int LBS_MULTIPLESEL = 0x0008;
        public const int LBS_OWNERDRAWFIXED = 0x0010;
        public const int LBS_OWNERDRAWVARIABLE = 0x0020;
        public const int LBS_HASSTRINGS = 0x0040;
        public const int LBS_USETABSTOPS = 0x0080;
        public const int LBS_NOINTEGRALHEIGHT = 0x0100;
        public const int LBS_MULTICOLUMN = 0x0200;
        public const int LBS_WANTKEYBOARDINPUT = 0x0400;
        public const int LBS_EXTENDEDSEL = 0x0800;
        public const int LBS_DISABLENOSCROLL = 0x1000;
        public const int LBS_NODATA = 0x2000;
        //#if(WINVER >= 0x0400);
        public const int LBS_NOSEL = 0x4000;
        //#endif; /* WINVER >= 0x0400 */
        public const int LBS_COMBOBOX = 0x8000;

        public const int LBS_STANDARD = (LBS_NOTIFY | LBS_SORT | WS_VSCROLL | WS_BORDER);


        //#endif; /* !NOWINSTYLES */


        /*
         * Combo Box return Values
         */
        public const int CB_OKAY = 0;
        public const int CB_ERR = (-1);
        public const int CB_ERRSPACE = (-2);


        /*
         * Combo Box Notification Codes
         */
        public const int CBN_ERRSPACE = (-1);
        public const int CBN_SELCHANGE = 1;
        public const int CBN_DBLCLK = 2;
        public const int CBN_SETFOCUS = 3;
        public const int CBN_KILLFOCUS = 4;
        public const int CBN_EDITCHANGE = 5;
        public const int CBN_EDITUPDATE = 6;
        public const int CBN_DROPDOWN = 7;
        public const int CBN_CLOSEUP = 8;
        public const int CBN_SELENDOK = 9;
        public const int CBN_SELENDCANCEL = 10;

        //#ifndef NOWINSTYLES

        /*
         * Combo Box styles
         */
        public const int CBS_SIMPLE = 0x0001;
        public const int CBS_DROPDOWN = 0x0002;
        public const int CBS_DROPDOWNLIST = 0x0003;
        public const int CBS_OWNERDRAWFIXED = 0x0010;
        public const int CBS_OWNERDRAWVARIABLE = 0x0020;
        public const int CBS_AUTOHSCROLL = 0x0040;
        public const int CBS_OEMCONVERT = 0x0080;
        public const int CBS_SORT = 0x0100;
        public const int CBS_HASSTRINGS = 0x0200;
        public const int CBS_NOINTEGRALHEIGHT = 0x0400;
        public const int CBS_DISABLENOSCROLL = 0x0800;
        //#if(WINVER >= 0x0400);
        public const int CBS_UPPERCASE = 0x2000;
        public const int CBS_LOWERCASE = 0x4000;
        //#endif; /* WINVER >= 0x0400 */

        //#endif ; /* !NOWINSTYLES */


        /*
         * Combo Box messages
         */
        //#ifndef NOWINMESSAGES
        public const int CB_GETEDITSEL = 0x0140;
        public const int CB_LIMITTEXT = 0x0141;
        public const int CB_SETEDITSEL = 0x0142;
        public const int CB_ADDSTRING = 0x0143;
        public const int CB_DELETESTRING = 0x0144;
        public const int CB_DIR = 0x0145;
        public const int CB_GETCOUNT = 0x0146;
        public const int CB_GETCURSEL = 0x0147;
        public const int CB_GETLBTEXT = 0x0148;
        public const int CB_GETLBTEXTLEN = 0x0149;
        public const int CB_INSERTSTRING = 0x014A;
        public const int CB_RESETCONTENT = 0x014B;
        public const int CB_FINDSTRING = 0x014C;
        public const int CB_SELECTSTRING = 0x014D;
        public const int CB_SETCURSEL = 0x014E;
        public const int CB_SHOWDROPDOWN = 0x014F;
        public const int CB_GETITEMDATA = 0x0150;
        public const int CB_SETITEMDATA = 0x0151;
        public const int CB_GETDROPPEDCONTROLRECT = 0x0152;
        public const int CB_SETITEMHEIGHT = 0x0153;
        public const int CB_GETITEMHEIGHT = 0x0154;
        public const int CB_SETEXTENDEDUI = 0x0155;
        public const int CB_GETEXTENDEDUI = 0x0156;
        public const int CB_GETDROPPEDSTATE = 0x0157;
        public const int CB_FINDSTRINGEXACT = 0x0158;
        public const int CB_SETLOCALE = 0x0159;
        public const int CB_GETLOCALE = 0x015A;
        //#if(WINVER >= 0x0400);
        public const int CB_GETTOPINDEX = 0x015b;
        public const int CB_SETTOPINDEX = 0x015c;
        public const int CB_GETHORIZONTALEXTENT = 0x015d;
        public const int CB_SETHORIZONTALEXTENT = 0x015e;
        public const int CB_GETDROPPEDWIDTH = 0x015f;
        public const int CB_SETDROPPEDWIDTH = 0x0160;
        public const int CB_INITSTORAGE = 0x0161;
        //#if(_WIN32_WCE >= 0x0400);
        public const int CB_MULTIPLEADDSTRING = 0x0163;
        //#endif
        //#endif; /* WINVER >= 0x0400 */

        //#if(_WIN32_WINNT >= 0x0501);
        public const int CB_GETCOMBOBOXINFO = 0x0164;
        //#endif; /* _WIN32_WINNT >= 0x0501 */

        //..WINVER
        //#if(_WIN32_WINNT >= 0x0501);
        public const int CB_MSGMAX = 0x0165;
        //#elif(_WIN32_WCE >= 0x0400);
        //public const int CB_MSGMAX = 0x0163;
        ////#elif(WINVER >= 0x0400);
        //public const int CB_MSGMAX = 0x0162;
        ////#else
        //public const int CB_MSGMAX = 0x015B;
        //#endif
        //#endif ; /* !NOWINMESSAGES */



        //#ifndef NOWINSTYLES


        /*
         * Scroll Bar Styles
         */
        public const int SBS_HORZ = 0x0000;
        public const int SBS_VERT = 0x0001;
        public const int SBS_TOPALIGN = 0x0002;
        public const int SBS_LEFTALIGN = 0x0002;
        public const int SBS_BOTTOMALIGN = 0x0004;
        public const int SBS_RIGHTALIGN = 0x0004;
        public const int SBS_SIZEBOXTOPLEFTALIGN = 0x0002;
        public const int SBS_SIZEBOXBOTTOMRIGHTALIGN = 0x0004;
        public const int SBS_SIZEBOX = 0x0008;
        //#if(WINVER >= 0x0400);
        public const int SBS_SIZEGRIP = 0x0010;
        //#endif; /* WINVER >= 0x0400 */


        //#endif; /* !NOWINSTYLES */

        /*
         * Scroll bar messages
         */
        //#ifndef NOWINMESSAGES
        public const int SBM_SETPOS = 0x00E0; /*not in win3.1 */
        public const int SBM_GETPOS = 0x00E1; /*not in win3.1 */
        public const int SBM_SETRANGE = 0x00E2; /*not in win3.1 */
        public const int SBM_SETRANGEREDRAW = 0x00E6; /*not in win3.1 */
        public const int SBM_GETRANGE = 0x00E3; /*not in win3.1 */
        public const int SBM_ENABLE_ARROWS = 0x00E4; /*not in win3.1 */
        //#if(WINVER >= 0x0400);
        public const int SBM_SETSCROLLINFO = 0x00E9;
        public const int SBM_GETSCROLLINFO = 0x00EA;
        //#endif; /* WINVER >= 0x0400 */

        //#if(_WIN32_WINNT >= 0x0501);
        public const int SBM_GETSCROLLBARINFO = 0x00EB;
        //#endif; /* _WIN32_WINNT >= 0x0501 */

        //#if(WINVER >= 0x0400);
        public const int SIF_RANGE = 0x0001;
        public const int SIF_PAGE = 0x0002;
        public const int SIF_POS = 0x0004;
        public const int SIF_DISABLENOSCROLL = 0x0008;
        public const int SIF_TRACKPOS = 0x0010;
        public const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);



        /*
         * MDI client style bits
         */
        public const int MDIS_ALLCHILDSTYLES = 0x0001;

        /*
         * wParam Flags for WM_MDITILE and WM_MDICASCADE messages.
         */
        public const int MDITILE_VERTICAL = 0x0000; /*not in win3.1 */
        public const int MDITILE_HORIZONTAL = 0x0001; /*not in win3.1 */
        public const int MDITILE_SKIPDISABLED = 0x0002; /*not in win3.1 */
        //#if(_WIN32_WINNT >= 0x0500);
        public const int MDITILE_ZORDER = 0x0004;
        //#endif /* _WIN32_WINNT >= 0x0500 */


        /*
         * Commands to pass to WinHelp()
         */
        public const int HELP_CONTEXT = 0x0001; /* Display topic in ulTopic */
        public const int HELP_QUIT = 0x0002; /* Terminate help */
        public const int HELP_INDEX = 0x0003; /* Display index */
        public const int HELP_CONTENTS = 0x0003;
        public const int HELP_HELPONHELP = 0x0004; /* Display help on using help */
        public const int HELP_SETINDEX = 0x0005; /* Set current Index for multi index help */
        public const int HELP_SETCONTENTS = 0x0005;
        public const int HELP_CONTEXTPOPUP = 0x0008;
        public const int HELP_FORCEFILE = 0x0009;
        public const int HELP_KEY = 0x0101; /* Display topic for keyword in offabData */
        public const int HELP_COMMAND = 0x0102;
        public const int HELP_PARTIALKEY = 0x0105;
        public const int HELP_MULTIKEY = 0x0201;
        public const int HELP_SETWINPOS = 0x0203;
        //#if(WINVER >= 0x0400);
        public const int HELP_CONTEXTMENU = 0x000a;
        public const int HELP_FINDER = 0x000b;
        public const int HELP_WM_HELP = 0x000c;
        public const int HELP_SETPOPUP_POS = 0x000d;

        public const int HELP_TCARD = 0x8000;
        public const int HELP_TCARD_DATA = 0x0010;
        public const int HELP_TCARD_OTHER_CALLER = 0x0011;

        // These are in winhelp.h in Win95.
        public const int IDH_NO_HELP = 28440;
        public const int IDH_MISSING_CONTEXT = 28441; // Control doesn't have matching help context
        public const int IDH_GENERIC_HELP_BUTTON = 28442; // Property sheet help button
        public const int IDH_OK = 28443;
        public const int IDH_CANCEL = 28444;
        public const int IDH_HELP = 28445;

        //#endif /* WINVER >= 0x0400 */

        public const int GR_GDIOBJECTS = 0; /* Count of GDI objects */
        public const int GR_USEROBJECTS = 1; /* Count of USER objects */

        /*
         * Parameter for SystemParametersInfo()
         */

        public const int SPI_GETBEEP = 0x0001;
        public const int SPI_SETBEEP = 0x0002;
        public const int SPI_GETMOUSE = 0x0003;
        public const int SPI_SETMOUSE = 0x0004;
        public const int SPI_GETBORDER = 0x0005;
        public const int SPI_SETBORDER = 0x0006;
        public const int SPI_GETKEYBOARDSPEED = 0x000A;
        public const int SPI_SETKEYBOARDSPEED = 0x000B;
        public const int SPI_LANGDRIVER = 0x000C;
        public const int SPI_ICONHORIZONTALSPACING = 0x000D;
        public const int SPI_GETSCREENSAVETIMEOUT = 0x000E;
        public const int SPI_SETSCREENSAVETIMEOUT = 0x000F;
        public const int SPI_GETSCREENSAVEACTIVE = 0x0010;
        public const int SPI_SETSCREENSAVEACTIVE = 0x0011;
        public const int SPI_GETGRIDGRANULARITY = 0x0012;
        public const int SPI_SETGRIDGRANULARITY = 0x0013;
        public const int SPI_SETDESKWALLPAPER = 0x0014;
        public const int SPI_SETDESKPATTERN = 0x0015;
        public const int SPI_GETKEYBOARDDELAY = 0x0016;
        public const int SPI_SETKEYBOARDDELAY = 0x0017;
        public const int SPI_ICONVERTICALSPACING = 0x0018;
        public const int SPI_GETICONTITLEWRAP = 0x0019;
        public const int SPI_SETICONTITLEWRAP = 0x001A;
        public const int SPI_GETMENUDROPALIGNMENT = 0x001B;
        public const int SPI_SETMENUDROPALIGNMENT = 0x001C;
        public const int SPI_SETDOUBLECLKWIDTH = 0x001D;
        public const int SPI_SETDOUBLECLKHEIGHT = 0x001E;
        public const int SPI_GETICONTITLELOGFONT = 0x001F;
        public const int SPI_SETDOUBLECLICKTIME = 0x0020;
        public const int SPI_SETMOUSEBUTTONSWAP = 0x0021;
        public const int SPI_SETICONTITLELOGFONT = 0x0022;
        public const int SPI_GETFASTTASKSWITCH = 0x0023;
        public const int SPI_SETFASTTASKSWITCH = 0x0024;
        //#if(WINVER >= 0x0400);
        public const int SPI_SETDRAGFULLWINDOWS = 0x0025;
        public const int SPI_GETDRAGFULLWINDOWS = 0x0026;
        public const int SPI_GETNONCLIENTMETRICS = 0x0029;
        public const int SPI_SETNONCLIENTMETRICS = 0x002A;
        public const int SPI_GETMINIMIZEDMETRICS = 0x002B;
        public const int SPI_SETMINIMIZEDMETRICS = 0x002C;
        public const int SPI_GETICONMETRICS = 0x002D;
        public const int SPI_SETICONMETRICS = 0x002E;
        public const int SPI_SETWORKAREA = 0x002F;
        public const int SPI_GETWORKAREA = 0x0030;
        public const int SPI_SETPENWINDOWS = 0x0031;

        public const int SPI_GETHIGHCONTRAST = 0x0042;
        public const int SPI_SETHIGHCONTRAST = 0x0043;
        public const int SPI_GETKEYBOARDPREF = 0x0044;
        public const int SPI_SETKEYBOARDPREF = 0x0045;
        public const int SPI_GETSCREENREADER = 0x0046;
        public const int SPI_SETSCREENREADER = 0x0047;
        public const int SPI_GETANIMATION = 0x0048;
        public const int SPI_SETANIMATION = 0x0049;
        public const int SPI_GETFONTSMOOTHING = 0x004A;
        public const int SPI_SETFONTSMOOTHING = 0x004B;
        public const int SPI_SETDRAGWIDTH = 0x004C;
        public const int SPI_SETDRAGHEIGHT = 0x004D;
        public const int SPI_SETHANDHELD = 0x004E;
        public const int SPI_GETLOWPOWERTIMEOUT = 0x004F;
        public const int SPI_GETPOWEROFFTIMEOUT = 0x0050;
        public const int SPI_SETLOWPOWERTIMEOUT = 0x0051;
        public const int SPI_SETPOWEROFFTIMEOUT = 0x0052;
        public const int SPI_GETLOWPOWERACTIVE = 0x0053;
        public const int SPI_GETPOWEROFFACTIVE = 0x0054;
        public const int SPI_SETLOWPOWERACTIVE = 0x0055;
        public const int SPI_SETPOWEROFFACTIVE = 0x0056;
        public const int SPI_SETCURSORS = 0x0057;
        public const int SPI_SETICONS = 0x0058;
        public const int SPI_GETDEFAULTINPUTLANG = 0x0059;
        public const int SPI_SETDEFAULTINPUTLANG = 0x005A;
        public const int SPI_SETLANGTOGGLE = 0x005B;
        public const int SPI_GETWINDOWSEXTENSION = 0x005C;
        public const int SPI_SETMOUSETRAILS = 0x005D;
        public const int SPI_GETMOUSETRAILS = 0x005E;
        public const int SPI_SETSCREENSAVERRUNNING = 0x0061;
        public const int SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING;
        //#endif /* WINVER >= 0x0400 */
        public const int SPI_GETFILTERKEYS = 0x0032;
        public const int SPI_SETFILTERKEYS = 0x0033;
        public const int SPI_GETTOGGLEKEYS = 0x0034;
        public const int SPI_SETTOGGLEKEYS = 0x0035;
        public const int SPI_GETMOUSEKEYS = 0x0036;
        public const int SPI_SETMOUSEKEYS = 0x0037;
        public const int SPI_GETSHOWSOUNDS = 0x0038;
        public const int SPI_SETSHOWSOUNDS = 0x0039;
        public const int SPI_GETSTICKYKEYS = 0x003A;
        public const int SPI_SETSTICKYKEYS = 0x003B;
        public const int SPI_GETACCESSTIMEOUT = 0x003C;
        public const int SPI_SETACCESSTIMEOUT = 0x003D;
        //#if(WINVER >= 0x0400);
        public const int SPI_GETSERIALKEYS = 0x003E;
        public const int SPI_SETSERIALKEYS = 0x003F;
        //#endif /* WINVER >= 0x0400 */
        public const int SPI_GETSOUNDSENTRY = 0x0040;
        public const int SPI_SETSOUNDSENTRY = 0x0041;
        //#if(_WIN32_WINNT >= 0x0400);
        public const int SPI_GETSNAPTODEFBUTTON = 0x005F;
        public const int SPI_SETSNAPTODEFBUTTON = 0x0060;
        //#endif /* _WIN32_WINNT >= 0x0400 */
        //#if (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > = 0x0400);
        public const int SPI_GETMOUSEHOVERWIDTH = 0x0062;
        public const int SPI_SETMOUSEHOVERWIDTH = 0x0063;
        public const int SPI_GETMOUSEHOVERHEIGHT = 0x0064;
        public const int SPI_SETMOUSEHOVERHEIGHT = 0x0065;
        public const int SPI_GETMOUSEHOVERTIME = 0x0066;
        public const int SPI_SETMOUSEHOVERTIME = 0x0067;
        public const int SPI_GETWHEELSCROLLLINES = 0x0068;
        public const int SPI_SETWHEELSCROLLLINES = 0x0069;
        public const int SPI_GETMENUSHOWDELAY = 0x006A;
        public const int SPI_SETMENUSHOWDELAY = 0x006B;


        public const int SPI_GETSHOWIMEUI = 0x006E;
        public const int SPI_SETSHOWIMEUI = 0x006F;
        //#endif


        //#if(WINVER >= 0x0500);
        public const int SPI_GETMOUSESPEED = 0x0070;
        public const int SPI_SETMOUSESPEED = 0x0071;
        public const int SPI_GETSCREENSAVERRUNNING = 0x0072;
        public const int SPI_GETDESKWALLPAPER = 0x0073;
        //#endif /* WINVER >= 0x0500 */


        //#if(WINVER >= 0x0500);
        public const int SPI_GETACTIVEWINDOWTRACKING = 0x1000;
        public const int SPI_SETACTIVEWINDOWTRACKING = 0x1001;
        public const int SPI_GETMENUANIMATION = 0x1002;
        public const int SPI_SETMENUANIMATION = 0x1003;
        public const int SPI_GETCOMBOBOXANIMATION = 0x1004;
        public const int SPI_SETCOMBOBOXANIMATION = 0x1005;
        public const int SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006;
        public const int SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007;
        public const int SPI_GETGRADIENTCAPTIONS = 0x1008;
        public const int SPI_SETGRADIENTCAPTIONS = 0x1009;
        public const int SPI_GETKEYBOARDCUES = 0x100A;
        public const int SPI_SETKEYBOARDCUES = 0x100B;
        public const int SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES;
        public const int SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES;
        public const int SPI_GETACTIVEWNDTRKZORDER = 0x100C;
        public const int SPI_SETACTIVEWNDTRKZORDER = 0x100D;
        public const int SPI_GETHOTTRACKING = 0x100E;
        public const int SPI_SETHOTTRACKING = 0x100F;
        public const int SPI_GETMENUFADE = 0x1012;
        public const int SPI_SETMENUFADE = 0x1013;
        public const int SPI_GETSELECTIONFADE = 0x1014;
        public const int SPI_SETSELECTIONFADE = 0x1015;
        public const int SPI_GETTOOLTIPANIMATION = 0x1016;
        public const int SPI_SETTOOLTIPANIMATION = 0x1017;
        public const int SPI_GETTOOLTIPFADE = 0x1018;
        public const int SPI_SETTOOLTIPFADE = 0x1019;
        public const int SPI_GETCURSORSHADOW = 0x101A;
        public const int SPI_SETCURSORSHADOW = 0x101B;
        //#if(_WIN32_WINNT >= 0x0501);
        public const int SPI_GETMOUSESONAR = 0x101C;
        public const int SPI_SETMOUSESONAR = 0x101D;
        public const int SPI_GETMOUSECLICKLOCK = 0x101E;
        public const int SPI_SETMOUSECLICKLOCK = 0x101F;
        public const int SPI_GETMOUSEVANISH = 0x1020;
        public const int SPI_SETMOUSEVANISH = 0x1021;
        public const int SPI_GETFLATMENU = 0x1022;
        public const int SPI_SETFLATMENU = 0x1023;
        public const int SPI_GETDROPSHADOW = 0x1024;
        public const int SPI_SETDROPSHADOW = 0x1025;
        public const int SPI_GETBLOCKSENDINPUTRESETS = 0x1026;
        public const int SPI_SETBLOCKSENDINPUTRESETS = 0x1027;
        //#endif /* _WIN32_WINNT >= 0x0501 */
        public const int SPI_GETUIEFFECTS = 0x103E;
        public const int SPI_SETUIEFFECTS = 0x103F;

        public const int SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000;
        public const int SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001;
        public const int SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002;
        public const int SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003;
        public const int SPI_GETFOREGROUNDFLASHCOUNT = 0x2004;
        public const int SPI_SETFOREGROUNDFLASHCOUNT = 0x2005;
        public const int SPI_GETCARETWIDTH = 0x2006;
        public const int SPI_SETCARETWIDTH = 0x2007;

        //#if(_WIN32_WINNT >= 0x0501);
        public const int SPI_GETMOUSECLICKLOCKTIME = 0x2008;
        public const int SPI_SETMOUSECLICKLOCKTIME = 0x2009;
        public const int SPI_GETFONTSMOOTHINGTYPE = 0x200A;
        public const int SPI_SETFONTSMOOTHINGTYPE = 0x200B;

        /* constants for SPI_GETFONTSMOOTHINGTYPE and SPI_SETFONTSMOOTHINGTYPE: */
        public const int FE_FONTSMOOTHINGSTANDARD = 0x0001;
        public const int FE_FONTSMOOTHINGCLEARTYPE = 0x0002;
        public const int FE_FONTSMOOTHINGDOCKING = 0x8000;

        public const int SPI_GETFONTSMOOTHINGCONTRAST = 0x200C;
        public const int SPI_SETFONTSMOOTHINGCONTRAST = 0x200D;

        public const int SPI_GETFOCUSBORDERWIDTH = 0x200E;
        public const int SPI_SETFOCUSBORDERWIDTH = 0x200F;
        public const int SPI_GETFOCUSBORDERHEIGHT = 0x2010;
        public const int SPI_SETFOCUSBORDERHEIGHT = 0x2011;

        public const int SPI_GETFONTSMOOTHINGORIENTATION = 0x2012;
        public const int SPI_SETFONTSMOOTHINGORIENTATION = 0x2013;

        /* constants for SPI_GETFONTSMOOTHINGORIENTATION and SPI_SETFONTSMOOTHINGORIENTATION: */
        public const int FE_FONTSMOOTHINGORIENTATIONBGR = 0x0000;
        public const int FE_FONTSMOOTHINGORIENTATIONRGB = 0x0001;
        //#endif /* _WIN32_WINNT >= 0x0501 */

        //#endif /* WINVER >= 0x0500 */

        /*
         * Flags
         */
        public const int SPIF_UPDATEINIFILE = 0x0001;
        public const int SPIF_SENDWININICHANGE = 0x0002;
        public const int SPIF_SENDCHANGE = SPIF_SENDWININICHANGE;

        public const int METRICS_USEDEFAULT = -1;

        public const int ARW_BOTTOMLEFT = 0x0000;
        public const int ARW_BOTTOMRIGHT = 0x0001;
        public const int ARW_TOPLEFT = 0x0002;
        public const int ARW_TOPRIGHT = 0x0003;
        public const int ARW_STARTMASK = 0x0003;
        public const int ARW_STARTRIGHT = 0x0001;
        public const int ARW_STARTTOP = 0x0002;

        public const int ARW_LEFT = 0x0000;
        public const int ARW_RIGHT = 0x0000;
        public const int ARW_UP = 0x0004;
        public const int ARW_DOWN = 0x0004;
        public const int ARW_HIDE = 0x0008;

        /* flags for SERIALKEYS dwFlags field */
        public const int SERKF_SERIALKEYSON = 0x00000001;
        public const int SERKF_AVAILABLE = 0x00000002;
        public const int SERKF_INDICATOR = 0x00000004;

        /* flags for HIGHCONTRAST dwFlags field */
        public const int HCF_HIGHCONTRASTON = 0x00000001;
        public const int HCF_AVAILABLE = 0x00000002;
        public const int HCF_HOTKEYACTIVE = 0x00000004;
        public const int HCF_CONFIRMHOTKEY = 0x00000008;
        public const int HCF_HOTKEYSOUND = 0x00000010;
        public const int HCF_INDICATOR = 0x00000020;
        public const int HCF_HOTKEYAVAILABLE = 0x00000040;
        public const int HCF_LOGONDESKTOP = 0x00000100;
        public const int HCF_DEFAULTDESKTOP = 0x00000200;

        /* Flags for ChangeDisplaySettings */
        public const int CDS_UPDATEREGISTRY = 0x00000001;
        public const int CDS_TEST = 0x00000002;
        public const int CDS_FULLSCREEN = 0x00000004;
        public const int CDS_GLOBAL = 0x00000008;
        public const int CDS_SET_PRIMARY = 0x00000010;
        public const int CDS_VIDEOPARAMETERS = 0x00000020;
        public const int CDS_RESET = 0x40000000;
        public const int CDS_NORESET = 0x10000000;


        /* Return values for ChangeDisplaySettings */
        public const int DISP_CHANGE_SUCCESSFUL = 0;
        public const int DISP_CHANGE_RESTART = 1;
        public const int DISP_CHANGE_FAILED = -1;
        public const int DISP_CHANGE_BADMODE = -2;
        public const int DISP_CHANGE_NOTUPDATED = -3;
        public const int DISP_CHANGE_BADFLAGS = -4;
        public const int DISP_CHANGE_BADPARAM = -5;
        //#if(_WIN32_WINNT >= 0x0501);
        public const int DISP_CHANGE_BADDUALVIEW = -6;
        //#endif /* _WIN32_WINNT >= 0x0501 */

        /* Flags for EnumDisplaySettingsEx */
        public const int EDS_RAWMODE = 0x00000002;


        /*
         * FILTERKEYS dwFlags field
         */
        public const int FKF_FILTERKEYSON = 0x00000001;
        public const int FKF_AVAILABLE = 0x00000002;
        public const int FKF_HOTKEYACTIVE = 0x00000004;
        public const int FKF_CONFIRMHOTKEY = 0x00000008;
        public const int FKF_HOTKEYSOUND = 0x00000010;
        public const int FKF_INDICATOR = 0x00000020;
        public const int FKF_CLICKON = 0x00000040;

        /*
         * STICKYKEYS dwFlags field
         */
        public const int SKF_STICKYKEYSON = 0x00000001;
        public const int SKF_AVAILABLE = 0x00000002;
        public const int SKF_HOTKEYACTIVE = 0x00000004;
        public const int SKF_CONFIRMHOTKEY = 0x00000008;
        public const int SKF_HOTKEYSOUND = 0x00000010;
        public const int SKF_INDICATOR = 0x00000020;
        public const int SKF_AUDIBLEFEEDBACK = 0x00000040;
        public const int SKF_TRISTATE = 0x00000080;
        public const int SKF_TWOKEYSOFF = 0x00000100;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int SKF_LALTLATCHED = 0x10000000;
        public const int SKF_LCTLLATCHED = 0x04000000;
        public const int SKF_LSHIFTLATCHED = 0x01000000;
        public const int SKF_RALTLATCHED = 0x20000000;
        public const int SKF_RCTLLATCHED = 0x08000000;
        public const int SKF_RSHIFTLATCHED = 0x02000000;
        public const int SKF_LWINLATCHED = 0x40000000;
        public const uint SKF_RWINLATCHED = 0x80000000;
        public const int SKF_LALTLOCKED = 0x00100000;
        public const int SKF_LCTLLOCKED = 0x00040000;
        public const int SKF_LSHIFTLOCKED = 0x00010000;
        public const int SKF_RALTLOCKED = 0x00200000;
        public const int SKF_RCTLLOCKED = 0x00080000;
        public const int SKF_RSHIFTLOCKED = 0x00020000;
        public const int SKF_LWINLOCKED = 0x00400000;
        public const int SKF_RWINLOCKED = 0x00800000;
        //#endif /* _WIN32_WINNT >= 0x0500 */


        /*
         * MOUSEKEYS dwFlags field
         */
        public const int MKF_MOUSEKEYSON = 0x00000001;
        public const int MKF_AVAILABLE = 0x00000002;
        public const int MKF_HOTKEYACTIVE = 0x00000004;
        public const int MKF_CONFIRMHOTKEY = 0x00000008;
        public const int MKF_HOTKEYSOUND = 0x00000010;
        public const int MKF_INDICATOR = 0x00000020;
        public const int MKF_MODIFIERS = 0x00000040;
        public const int MKF_REPLACENUMBERS = 0x00000080;
        //#if(_WIN32_WINNT >= 0x0500);
        public const int MKF_LEFTBUTTONSEL = 0x10000000;
        public const int MKF_RIGHTBUTTONSEL = 0x20000000;
        public const int MKF_LEFTBUTTONDOWN = 0x01000000;
        public const int MKF_RIGHTBUTTONDOWN = 0x02000000;
        public const uint MKF_MOUSEMODE = 0x80000000;
        //#endif /* _WIN32_WINNT >= 0x0500 */

        /*
         * ACCESSTIMEOUT dwFlags field
         */
        public const int ATF_TIMEOUTON = 0x00000001;
        public const int ATF_ONOFFFEEDBACK = 0x00000002;

        /* values for SOUNDSENTRY iFSGrafEffect field */
        public const int SSGF_NONE = 0;
        public const int SSGF_DISPLAY = 3;

        /* values for SOUNDSENTRY iFSTextEffect field */
        public const int SSTF_NONE = 0;
        public const int SSTF_CHARS = 1;
        public const int SSTF_BORDER = 2;
        public const int SSTF_DISPLAY = 3;

        /* values for SOUNDSENTRY iWindowsEffect field */
        public const int SSWF_NONE = 0;
        public const int SSWF_TITLE = 1;
        public const int SSWF_WINDOW = 2;
        public const int SSWF_DISPLAY = 3;
        public const int SSWF_CUSTOM = 4;

        /*
         * SOUNDSENTRY dwFlags field
         */
        public const int SSF_SOUNDSENTRYON = 0x00000001;
        public const int SSF_AVAILABLE = 0x00000002;
        public const int SSF_INDICATOR = 0x00000004;


        /*
         * TOGGLEKEYS dwFlags field
         */
        public const int TKF_TOGGLEKEYSON = 0x00000001;
        public const int TKF_AVAILABLE = 0x00000002;
        public const int TKF_HOTKEYACTIVE = 0x00000004;
        public const int TKF_CONFIRMHOTKEY = 0x00000008;
        public const int TKF_HOTKEYSOUND = 0x00000010;
        public const int TKF_INDICATOR = 0x00000020;


        /*
         * SetLastErrorEx() types.
         */

        public const int SLE_ERROR = 0x00000001;
        public const int SLE_MINORERROR = 0x00000002;
        public const int SLE_WARNING = 0x00000003;


        /*
         * Multimonitor API.
         */

        public const int MONITOR_DEFAULTTONULL = 0x00000000;
        public const int MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        public const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        public const int MONITORINFOF_PRIMARY = 0x00000001;

        //#ifndef CCHDEVICENAME
        public const int CCHDEVICENAME = 32;
        //#endif


        /*
         * dwFlags for SetWinEventHook
         */
        public const int WINEVENT_OUTOFCONTEXT = 0x0000; // Events are ASYNC
        public const int WINEVENT_SKIPOWNTHREAD = 0x0001; // Don't call back for events on installer's thread
        public const int WINEVENT_SKIPOWNPROCESS = 0x0002; // Don't call back for events on installer's process
        public const int WINEVENT_INCONTEXT = 0x0004; // Events are SYNC, this causes your dll to be injected into every process


        /*
         * Common object IDs (cookies, only for sending WM_GETOBJECT to get at the
         * thing in question). = Positive IDs are reserved for apps (app specific),
         * negative IDs are system things and are global, 0 means "just little old
         * me".
         */
        public const int CHILDID_SELF = 0;
        public const int INDEXID_OBJECT = 0;
        public const int INDEXID_CONTAINER = 0;

        /*
         * Reserved IDs for system objects
         */
        public const int OBJID_WINDOW = (0x00000000);
        public const uint OBJID_SYSMENU = (0xFFFFFFFF);
        public const uint OBJID_TITLEBAR = (0xFFFFFFFE);
        public const uint OBJID_MENU = (0xFFFFFFFD);
        public const uint OBJID_CLIENT = (0xFFFFFFFC);
        public const uint OBJID_VSCROLL = (0xFFFFFFFB);
        public const uint OBJID_HSCROLL = (0xFFFFFFFA);
        public const uint OBJID_SIZEGRIP = (0xFFFFFFF9);
        public const uint OBJID_CARET = (0xFFFFFFF8);
        public const uint OBJID_CURSOR = (0xFFFFFFF7);
        public const uint OBJID_ALERT = (0xFFFFFFF6);
        public const uint OBJID_SOUND = (0xFFFFFFF5);
        public const uint OBJID_QUERYCLASSNAMEIDX = 0xFFFFFFF4;
        public const uint OBJID_NATIVEOM = 0xFFFFFFF0;

        /*
         * EVENT DEFINITION
         */
        public const int EVENT_MIN = 0x00000001;
        public const int EVENT_MAX = 0x7FFFFFFF;

        public const int EVENT_SYSTEM_SOUND = 0x0001;

        public const int EVENT_SYSTEM_ALERT = 0x0002;

        public const int EVENT_SYSTEM_FOREGROUND = 0x0003;

        /*
         * Menu
         * = hwnd = is window (top level window or popup menu window);
         * = idObject = is ID of control (OBJID_MENU, OBJID_SYSMENU, OBJID_SELF for popup)
         * = idChild = is CHILDID_SELF;
         *
         * EVENT_SYSTEM_MENUSTART
         * EVENT_SYSTEM_MENUEND
         * For MENUSTART, hwnd+idObject+idChild refers to the control with the menu bar,
         * = or the control bringing up the context menu.
         *
         * Sent when entering into and leaving from menu mode (system, app bar, and
         * track popups).
         */
        public const int EVENT_SYSTEM_MENUSTART = 0x0004;
        public const int EVENT_SYSTEM_MENUEND = 0x0005;

        /*
         * EVENT_SYSTEM_MENUPOPUPSTART
         * EVENT_SYSTEM_MENUPOPUPEND
         * Sent when a menu popup comes up and just before it is taken down. = Note;
         * that for a call to TrackPopupMenu(), a client will see EVENT_SYSTEM_MENUSTART
         * followed almost immediately by EVENT_SYSTEM_MENUPOPUPSTART for the popup
         * being shown.
         *
         * For MENUPOPUP, hwnd+idObject+idChild refers to the NEW popup coming up, not the
         * parent item which is hierarchical. = You can get the parent menu/popup by
         * asking for the accParent object.
         */
        public const int EVENT_SYSTEM_MENUPOPUPSTART = 0x0006;
        public const int EVENT_SYSTEM_MENUPOPUPEND = 0x0007;


        /*
         * EVENT_SYSTEM_CAPTURESTART
         * EVENT_SYSTEM_CAPTUREEND
         * Sent when a window takes the capture and releases the capture.
         */
        public const int EVENT_SYSTEM_CAPTURESTART = 0x0008;
        public const int EVENT_SYSTEM_CAPTUREEND = 0x0009;

        /*
         * Move Size
         * EVENT_SYSTEM_MOVESIZESTART
         * EVENT_SYSTEM_MOVESIZEEND
         * Sent when a window enters and leaves move-size dragging mode.
         */
        public const int EVENT_SYSTEM_MOVESIZESTART = 0x000A;
        public const int EVENT_SYSTEM_MOVESIZEEND = 0x000B;

        /*
         * Context Help
         * EVENT_SYSTEM_CONTEXTHELPSTART
         * EVENT_SYSTEM_CONTEXTHELPEND
         * Sent when a window enters and leaves context sensitive help mode.
         */
        public const int EVENT_SYSTEM_CONTEXTHELPSTART = 0x000C;
        public const int EVENT_SYSTEM_CONTEXTHELPEND = 0x000D;

        /*
         * Drag & Drop
         * EVENT_SYSTEM_DRAGDROPSTART
         * EVENT_SYSTEM_DRAGDROPEND
         * Send the START notification just before going into drag&drop loop. = Send;
         * the END notification just after canceling out.
         * Note that it is up to apps and OLE to generate this, since the system
         * doesn't know. = Like EVENT_SYSTEM_SOUND, it will be a while before this
         * is prevalent.
         */
        public const int EVENT_SYSTEM_DRAGDROPSTART = 0x000E;
        public const int EVENT_SYSTEM_DRAGDROPEND = 0x000F;

        /*
         * Dialog
         * Send the START notification right after the dialog is completely
         * = initialized and visible. = Send the END right before the dialog;
         * = is hidden and goes away.
         * EVENT_SYSTEM_DIALOGSTART
         * EVENT_SYSTEM_DIALOGEND
         */
        public const int EVENT_SYSTEM_DIALOGSTART = 0x0010;
        public const int EVENT_SYSTEM_DIALOGEND = 0x0011;

        /*
         * EVENT_SYSTEM_SCROLLING
         * EVENT_SYSTEM_SCROLLINGSTART
         * EVENT_SYSTEM_SCROLLINGEND
         * Sent when beginning and ending the tracking of a scrollbar in a window,
         * and also for scrollbar controls.
         */
        public const int EVENT_SYSTEM_SCROLLINGSTART = 0x0012;
        public const int EVENT_SYSTEM_SCROLLINGEND = 0x0013;

        /*
         * Alt-Tab Window
         * Send the START notification right after the switch window is initialized
         * and visible. = Send the END right before it is hidden and goes away.
         * EVENT_SYSTEM_SWITCHSTART
         * EVENT_SYSTEM_SWITCHEND
         */
        public const int EVENT_SYSTEM_SWITCHSTART = 0x0014;
        public const int EVENT_SYSTEM_SWITCHEND = 0x0015;

        /*
         * EVENT_SYSTEM_MINIMIZESTART
         * EVENT_SYSTEM_MINIMIZEEND
         * Sent when a window minimizes and just before it restores.
         */
        public const int EVENT_SYSTEM_MINIMIZESTART = 0x0016;
        public const int EVENT_SYSTEM_MINIMIZEEND = 0x0017;


        //#if(_WIN32_WINNT >= 0x0501);
        public const int EVENT_CONSOLE_CARET = 0x4001;
        public const int EVENT_CONSOLE_UPDATE_REGION = 0x4002;
        public const int EVENT_CONSOLE_UPDATE_SIMPLE = 0x4003;
        public const int EVENT_CONSOLE_UPDATE_SCROLL = 0x4004;
        public const int EVENT_CONSOLE_LAYOUT = 0x4005;
        public const int EVENT_CONSOLE_START_APPLICATION = 0x4006;
        public const int EVENT_CONSOLE_END_APPLICATION = 0x4007;

        /*
         * Flags for EVENT_CONSOLE_START/END_APPLICATION.
         */
        public const int CONSOLE_APPLICATION_16BIT = 0x0001;

        /*
         * Flags for EVENT_CONSOLE_CARET
         */
        public const int CONSOLE_CARET_SELECTION = 0x0001;
        public const int CONSOLE_CARET_VISIBLE = 0x0002;
        //#endif /* _WIN32_WINNT >= 0x0501 */

        public const int EVENT_OBJECT_CREATE = 0x8000; // hwnd + ID + idChild is created item
        public const int EVENT_OBJECT_DESTROY = 0x8001; // hwnd + ID + idChild is destroyed item
        public const int EVENT_OBJECT_SHOW = 0x8002; // hwnd + ID + idChild is shown item
        public const int EVENT_OBJECT_HIDE = 0x8003; // hwnd + ID + idChild is hidden item
        public const int EVENT_OBJECT_REORDER = 0x8004; // hwnd + ID + idChild is parent of zordering children



        public const int EVENT_OBJECT_FOCUS = 0x8005; // hwnd + ID + idChild is focused item
        public const int EVENT_OBJECT_SELECTION = 0x8006; // hwnd + ID + idChild is selected item (if only one), or idChild is OBJID_WINDOW if complex
        public const int EVENT_OBJECT_SELECTIONADD = 0x8007; // hwnd + ID + idChild is item added
        public const int EVENT_OBJECT_SELECTIONREMOVE = 0x8008; // hwnd + ID + idChild is item removed
        public const int EVENT_OBJECT_SELECTIONWITHIN = 0x8009; // hwnd + ID + idChild is parent of changed selected items



        public const int EVENT_OBJECT_STATECHANGE = 0x800A; // hwnd + ID + idChild is item w/ state change
        /*
         * Examples of when to send an EVENT_OBJECT_STATECHANGE include
         * = * It is being enabled/disabled (USER does for windows)
         * = * It is being pressed/released (USER does for buttons)
         * = * It is being checked/unchecked (USER does for radio/check buttons)
         */
        public const int EVENT_OBJECT_LOCATIONCHANGE = 0x800B; // hwnd + ID + idChild is moved/sized item



        public const int EVENT_OBJECT_NAMECHANGE = 0x800C; // hwnd + ID + idChild is item w/ name change
        public const int EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800D; // hwnd + ID + idChild is item w/ desc change
        public const int EVENT_OBJECT_VALUECHANGE = 0x800E; // hwnd + ID + idChild is item w/ value change
        public const int EVENT_OBJECT_PARENTCHANGE = 0x800F; // hwnd + ID + idChild is item w/ new parent
        public const int EVENT_OBJECT_HELPCHANGE = 0x8010; // hwnd + ID + idChild is item w/ help change
        public const int EVENT_OBJECT_DEFACTIONCHANGE = 0x8011; // hwnd + ID + idChild is item w/ def action change
        public const int EVENT_OBJECT_ACCELERATORCHANGE = 0x8012; // hwnd + ID + idChild is item w/ keybd accel change


        /*
         * Child IDs
         */

        /*
         * System Sounds (idChild of system SOUND notification)
         */
        public const int SOUND_SYSTEM_STARTUP = 1;
        public const int SOUND_SYSTEM_SHUTDOWN = 2;
        public const int SOUND_SYSTEM_BEEP = 3;
        public const int SOUND_SYSTEM_ERROR = 4;
        public const int SOUND_SYSTEM_QUESTION = 5;
        public const int SOUND_SYSTEM_WARNING = 6;
        public const int SOUND_SYSTEM_INFORMATION = 7;
        public const int SOUND_SYSTEM_MAXIMIZE = 8;
        public const int SOUND_SYSTEM_MINIMIZE = 9;
        public const int SOUND_SYSTEM_RESTOREUP = 10;
        public const int SOUND_SYSTEM_RESTOREDOWN = 11;
        public const int SOUND_SYSTEM_APPSTART = 12;
        public const int SOUND_SYSTEM_FAULT = 13;
        public const int SOUND_SYSTEM_APPEND = 14;
        public const int SOUND_SYSTEM_MENUCOMMAND = 15;
        public const int SOUND_SYSTEM_MENUPOPUP = 16;
        public const int CSOUND_SYSTEM = 16;

        /*
         * System Alerts (indexChild of system ALERT notification)
         */
        public const int ALERT_SYSTEM_INFORMATIONAL = 1; // MB_INFORMATION
        public const int ALERT_SYSTEM_WARNING = 2; // MB_WARNING
        public const int ALERT_SYSTEM_ERROR = 3; // MB_ERROR
        public const int ALERT_SYSTEM_QUERY = 4; // MB_QUESTION
        public const int ALERT_SYSTEM_CRITICAL = 5; // HardSysErrBox
        public const int CALERT_SYSTEM = 6;


        //#ifndef NO_STATE_FLAGS
        public const int STATE_SYSTEM_UNAVAILABLE = 0x00000001; // Disabled
        public const int STATE_SYSTEM_SELECTED = 0x00000002;
        public const int STATE_SYSTEM_FOCUSED = 0x00000004;
        public const int STATE_SYSTEM_PRESSED = 0x00000008;
        public const int STATE_SYSTEM_CHECKED = 0x00000010;
        public const int STATE_SYSTEM_MIXED = 0x00000020; // 3-state checkbox or toolbar button
        public const int STATE_SYSTEM_INDETERMINATE = STATE_SYSTEM_MIXED;
        public const int STATE_SYSTEM_READONLY = 0x00000040;
        public const int STATE_SYSTEM_HOTTRACKED = 0x00000080;
        public const int STATE_SYSTEM_DEFAULT = 0x00000100;
        public const int STATE_SYSTEM_EXPANDED = 0x00000200;
        public const int STATE_SYSTEM_COLLAPSED = 0x00000400;
        public const int STATE_SYSTEM_BUSY = 0x00000800;
        public const int STATE_SYSTEM_FLOATING = 0x00001000; // Children "owned" not "contained" by parent
        public const int STATE_SYSTEM_MARQUEED = 0x00002000;
        public const int STATE_SYSTEM_ANIMATED = 0x00004000;
        public const int STATE_SYSTEM_INVISIBLE = 0x00008000;
        public const int STATE_SYSTEM_OFFSCREEN = 0x00010000;
        public const int STATE_SYSTEM_SIZEABLE = 0x00020000;
        public const int STATE_SYSTEM_MOVEABLE = 0x00040000;
        public const int STATE_SYSTEM_SELFVOICING = 0x00080000;
        public const int STATE_SYSTEM_FOCUSABLE = 0x00100000;
        public const int STATE_SYSTEM_SELECTABLE = 0x00200000;
        public const int STATE_SYSTEM_LINKED = 0x00400000;
        public const int STATE_SYSTEM_TRAVERSED = 0x00800000;
        public const int STATE_SYSTEM_MULTISELECTABLE = 0x01000000; // Supports multiple selection
        public const int STATE_SYSTEM_EXTSELECTABLE = 0x02000000; // Supports extended selection
        public const int STATE_SYSTEM_ALERT_LOW = 0x04000000; // This information is of low priority
        public const int STATE_SYSTEM_ALERT_MEDIUM = 0x08000000; // This information is of medium priority
        public const int STATE_SYSTEM_ALERT_HIGH = 0x10000000; // This information is of high priority
        public const int STATE_SYSTEM_PROTECTED = 0x20000000; // access to this is restricted
        public const int STATE_SYSTEM_VALID = 0x3FFFFFFF;
        //#endif

        public const int CCHILDREN_TITLEBAR = 5;
        public const int CCHILDREN_SCROLLBAR = 5;


        public const int CURSOR_SHOWING = 0x00000001;


        public const int WS_ACTIVECAPTION = 0x0001;


        /*
         * The "real" ancestor window
         */
        public const int GA_PARENT = 1;
        public const int GA_ROOT = 2;
        public const int GA_ROOTOWNER = 3;


        /*
         * The input is in the regular message flow,
         * the app is required to call DefWindowProc
         * so that the system can perform clean ups.
         */
        public const int RIM_INPUT = 0;

        /*
         * The input is sink only. The app is expected
         * to behave nicely.
         */
        public const int RIM_INPUTSINK = 1;


        /*
         * Type of the raw input
         */
        public const int RIM_TYPEMOUSE = 0;
        public const int RIM_TYPEKEYBOARD = 1;
        public const int RIM_TYPEHID = 2;


        /*
         * Define the mouse button state indicators.
         */

        public const int RI_MOUSE_LEFT_BUTTON_DOWN = 0x0001; // Left Button changed to down.
        public const int RI_MOUSE_LEFT_BUTTON_UP = 0x0002; // Left Button changed to up.
        public const int RI_MOUSE_RIGHT_BUTTON_DOWN = 0x0004; // Right Button changed to down.
        public const int RI_MOUSE_RIGHT_BUTTON_UP = 0x0008; // Right Button changed to up.
        public const int RI_MOUSE_MIDDLE_BUTTON_DOWN = 0x0010; // Middle Button changed to down.
        public const int RI_MOUSE_MIDDLE_BUTTON_UP = 0x0020; // Middle Button changed to up.

        public const int RI_MOUSE_BUTTON_1_DOWN = RI_MOUSE_LEFT_BUTTON_DOWN;
        public const int RI_MOUSE_BUTTON_1_UP = RI_MOUSE_LEFT_BUTTON_UP;
        public const int RI_MOUSE_BUTTON_2_DOWN = RI_MOUSE_RIGHT_BUTTON_DOWN;
        public const int RI_MOUSE_BUTTON_2_UP = RI_MOUSE_RIGHT_BUTTON_UP;
        public const int RI_MOUSE_BUTTON_3_DOWN = RI_MOUSE_MIDDLE_BUTTON_DOWN;
        public const int RI_MOUSE_BUTTON_3_UP = RI_MOUSE_MIDDLE_BUTTON_UP;

        public const int RI_MOUSE_BUTTON_4_DOWN = 0x0040;
        public const int RI_MOUSE_BUTTON_4_UP = 0x0080;
        public const int RI_MOUSE_BUTTON_5_DOWN = 0x0100;
        public const int RI_MOUSE_BUTTON_5_UP = 0x0200;

        /*
         * If usButtonFlags has RI_MOUSE_WHEEL, the wheel delta is stored in usButtonData.
         * Take it as a signed value.
         */
        public const int RI_MOUSE_WHEEL = 0x0400;

        /*
         * Define the mouse indicator flags.
         */
        public const int MOUSE_MOVE_RELATIVE = 0;
        public const int MOUSE_MOVE_ABSOLUTE = 1;
        public const int MOUSE_VIRTUAL_DESKTOP = 0x02; // the coordinates are mapped to the virtual desktop
        public const int MOUSE_ATTRIBUTES_CHANGED = 0x04; // requery for mouse attributes

        public const int KEYBOARD_OVERRUN_MAKE_CODE = 0xFF;

        /*
         * Define the keyboard input data Flags.
         */
        public const int RI_KEY_MAKE = 0;
        public const int RI_KEY_BREAK = 1;
        public const int RI_KEY_E0 = 2;
        public const int RI_KEY_E1 = 4;
        public const int RI_KEY_TERMSRV_SET_LED = 8;
        public const int RI_KEY_TERMSRV_SHADOW = 0x10;


        /*
         * Flags for GetRawInputData
         */

        public const int RID_INPUT = 0x10000003;
        public const int RID_HEADER = 0x10000005;


        /*
         * Raw Input Device Information
         */
        public const int RIDI_PREPARSEDDATA = 0x20000005;
        public const int RIDI_DEVICENAME = 0x20000007; // the return valus is the character length, not the byte size
        public const int RIDI_DEVICEINFO = 0x2000000b;


        public const int RIDEV_REMOVE = 0x00000001;
        public const int RIDEV_EXCLUDE = 0x00000010;
        public const int RIDEV_PAGEONLY = 0x00000020;
        public const int RIDEV_NOLEGACY = 0x00000030;
        public const int RIDEV_INPUTSINK = 0x00000100;
        public const int RIDEV_CAPTUREMOUSE = 0x00000200; // effective when mouse nolegacy is specified, otherwise it would be an error
        public const int RIDEV_NOHOTKEYS = 0x00000200; // effective for keyboard.
        public const int RIDEV_APPKEYS = 0x00000400; // effective for keyboard.
        public const int RIDEV_EXMODEMASK = 0x000000F0;

    }
}
