﻿using MaaCommon.Interop;
using MaaCommon.Server;

class ControllerLogger : MaaControllerResponser
{
    public void ConnectSuccess(string uuid, int width, int height)
    {
        Console.WriteLine("### Connect Success! uuid: {0}  resolution: {1}x{2}", uuid, width, height);
    }
    public void ConnectFailed(string why)
    {
        Console.WriteLine("### Connect Failed! why:{0}", why);
    }
}

class MaaCommonTest
{
    public static void Main(string[] args)
    {
        var config = """
 {
    "prebuilt": {
        "minicap": {
            "root": "./MaaAgentBinary/minicap",
            "arch": [
                "x86",
                "armeabi-v7a",
                "armeabi"
            ],
            "sdk": [
                31,
                29,
                28,
                27,
                26,
                25,
                24,
                23,
                22,
                21,
                19,
                18,
                17,
                16,
                15,
                14
            ]
        },
        "minitouch": {
            "root": "./MaaAgentBinary/minitouch",
            "arch": [
                "x86_64",
                "x86",
                "arm64-v8a",
                "armeabi-v7a",
                "armeabi"
            ]
        },
        "maatouch": {
            "root": "./MaaAgentBinary/maatouch",
            "package": "com.shxyke.MaaTouch.App"
        }
    },
    "argv": {
        "Connect": [
            "{ADB}",
            "connect",
            "{ADB_SERIAL}"
        ],
        "KillServer": [
            "{ADB}",
            "kill-server"
        ],
        "UUID": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "settings get secure android_id"
        ],
        "Resolution": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "dumpsys window displays | grep -o -E cur=+[^\\ ]+ | grep -o -E [0-9]+"
        ],
        "StartApp": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "am start -n {INTENT}"
        ],
        "StopApp": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "am force-stop {INTENT}"
        ],
        "Click": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "input tap {X} {Y}"
        ],
        "Swipe": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "input swipe {X1} {Y1} {X2} {Y2} {DURATION}"
        ],
        "PressKey": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "input keyevent {KEY}"
        ],
        "ForwardSocket": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "forward",
            "tcp:{FOWARD_PORT}",
            "localabstract:{LOCAL_SOCKET}"
        ],
        "NetcatAddress": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "cat /proc/net/arp | grep : "
        ],
        "ScreencapRawByNetcat": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "exec-out",
            "screencap | nc -w 3 {NETCAT_ADDRESS} {NETCAT_PORT}"
        ],
        "ScreencapRawWithGzip": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "exec-out",
            "screencap | gzip -1"
        ],
        "ScreencapEncode": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "exec-out",
            "screencap -p"
        ],
        "ScreencapEncodeToFile": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "screencap -p > \"/data/local/tmp/{TEMP_FILE}\""
        ],
        "PullFile": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "pull",
            "/data/local/tmp/{TEMP_FILE}",
            "{DST_PATH}"
        ],
        "Abilist": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "getprop ro.product.cpu.abilist | tr -d '\n\r'"
        ],
        "SDK": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "getprop ro.build.version.sdk | tr -d '\n\r'"
        ],
        "Orientation": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "dumpsys input | grep SurfaceOrientation | grep -m 1 -o -E [0-9]"
        ],
        "PushBin": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "push",
            "{BIN_PATH}",
            "/data/local/tmp/{BIN_WORKING_FILE}"
        ],
        "ChmodBin": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "chmod 700 \"/data/local/tmp/{BIN_WORKING_FILE}\""
        ],
        "InvokeBin": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "export LD_LIBRARY_PATH=/data/local/tmp/; \"/data/local/tmp/{BIN_WORKING_FILE}\" {BIN_EXTRA_PARAMS}"
        ],
        "InvokeApp": [
            "{ADB}",
            "-s",
            "{ADB_SERIAL}",
            "shell",
            "export CLASSPATH=\"/data/local/tmp/{APP_WORKING_FILE}\"; app_process /data/local/tmp {PACKAGE_NAME}"
        ]
    }
}
""";

        Console.WriteLine(MaaProxy.MaaVersion());

        //using (var ctrl = MaaController.CreateAdb("adb", "emulator-5554", MaaProxy.AdbControllerType.Input_Preset_Adb | MaaProxy.AdbControllerType.Screencap_RawByNetcat, config, new ControllerLogger()))
        //{
        //    var id = MaaProxy.MaaControllerPostConnection(ctrl.GetHandle());
        //    MaaProxy.MaaControllerWait(ctrl.GetHandle(), id);

        //    Console.WriteLine("@@@ UUID: {0}", MaaProxy.MaaControllerGetUUID(ctrl.GetHandle()));

        //    id = MaaProxy.MaaControllerPostScreencap(ctrl.GetHandle());
        //    MaaProxy.MaaControllerWait(ctrl.GetHandle(), id);

        //    var arr = MaaProxy.MaaControllerGetImage(ctrl.GetHandle());
        //    if (arr != null)
        //    {
        //        File.WriteAllBytes("test.png", arr);
        //    }
        //}

        HttpService service = new HttpService();
        service.listen();
        while(true)
        {
            Thread.Sleep(5000);
        }
    }
}
