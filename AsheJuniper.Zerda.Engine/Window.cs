using AsheJuniper.Zerda.Engine;
using Raylib_cs;

public sealed class Window
{
    private const int DefaultWindowWidth = 640;
    private const int DefaultWindowHeight = 480;
    private const string DefaultWindowTitle = "Zerda Engine";

    internal static Window? instance;
    private static readonly object padlock = new object();

    private int targetFrameRate = 60;

    public ZerdaEngine EngineInstance { get; private set; }

    public static Window Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = ZerdaEngine.Instance.CreateWindow();
                }

                return instance;
            }
        }
    }

    public int FrameRate
    {
        get
        {
            return Raylib.GetFPS();
        }
    }

    public int TargetFrameRate
    {
        get
        {
            return targetFrameRate;
        }

        set
        {
            targetFrameRate = value;

            Raylib.SetTargetFPS(targetFrameRate);
        }
    }

    public bool ShouldExit
    {
        get
        {
            return Raylib.WindowShouldClose();
        }
    }

    public Window(
        ZerdaEngine zerdaEngineInstance,
        bool fullscreen=false,
        bool resizeable=true,
        bool undecorated=false
    )
    {
        EngineInstance = zerdaEngineInstance;

        Initialize(
            fullscreen,
            resizeable,
            undecorated
        );
    }

    public static Window Start(
        ZerdaEngine zerdaEngineInstance,
        bool fullscreen=false,
        bool resizeable=true,
        bool undecorated=false
    )
    {
        instance = new Window(
            zerdaEngineInstance,
            fullscreen,
            resizeable,
            undecorated
        );

        return Instance;
    }

    public void Clear(Color color)
    {
        Raylib.ClearBackground(color);
    }

    public void DrawText(
        string text,
        int windowPositionX,
        int windowPositionY,
        int fontSize,
        Color color
    )
    {
        Raylib.DrawText(
            text,
            windowPositionX,
            windowPositionY,
            fontSize,
            color
        );
    }

    public void DrawText(
        string text,
        System.Numerics.Vector2 position,
        int fontSize,
        Color color
    )
    {
        Raylib.DrawText(
            text,
            Convert.ToInt32(
                Math.Round(position.X,
                    MidpointRounding.AwayFromZero
                )
            ),
            Convert.ToInt32(
                Math.Round(position.Y,
                    MidpointRounding.AwayFromZero
                )
            ),
            fontSize,
            color
        );
    }

    internal void Render()
    {
        Raylib.BeginDrawing();

        Clear(
            Color.BLACK
        );

        DrawText(
            "Why are you not working?",
            16,
            16,
            24,
            Color.WHITE
        );

        // TODO: Render logic goes here.
        // TODO: Call all OnRender event listeners.

        Raylib.EndDrawing();
    }

    private void Initialize(
        bool fullscreen=false,
        bool resizeable=true,
        bool undecorated=false
    )
    {
        // Set window initialization flags.
        var configFlags = (ConfigFlags)0x00;

        // Should the window should be fullscreen?
        if (fullscreen)
        {
            configFlags |= ConfigFlags.FLAG_FULLSCREEN_MODE;
        }

        // Should the window should be resizeable?
        if (resizeable)
        {
            configFlags |= ConfigFlags.FLAG_WINDOW_RESIZABLE;
        }

        // Should the window should be resizeable?
        if (undecorated)
        {
            configFlags |= ConfigFlags.FLAG_WINDOW_UNDECORATED;
        }

        // Apply window initialization flags.
        Raylib.SetConfigFlags(configFlags);

        var windowWidth = (int)(Raylib.GetScreenWidth() * 0.66);
        var windowHeight = (int)(Raylib.GetScreenHeight() * 0.66);

        var windowTitle = DefaultWindowTitle;

        if (windowWidth < DefaultWindowWidth)
        {
            windowWidth = DefaultWindowWidth;
        }

        if (windowHeight < DefaultWindowHeight)
        {
            windowHeight = DefaultWindowHeight;
        }

        // Create a new window instance.
        Raylib.InitWindow(
            windowWidth,
            windowHeight,
            windowTitle
        );

        TargetFrameRate = 60;
    }
}
