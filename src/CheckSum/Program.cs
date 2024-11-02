﻿using CheckSum;
using CheckSum.Extensions;
using RtuTools.Extensions;

public static class Program
{
    public static void Main(string[] args)
    {
        var wData =
            "23820000100001003837303031383834343333000e00000000000000d8430c1f0c23011c150816193837373731383834343333001200000001000000020063636363636363630063383730373138383434333300160000000100000002000101010001050f1b171b31323334353637383930313233343536373839303132636363636363636300633231303938373635343332313039383736353433323103060100010511100f1b31323331323331323300000012000000010000000200636363636363636300630000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000042";
        var rData =
            "23830000100001003837303031383834343333001a000000010000000a000c1f0c23011c150816193837373731383834343333000e00000000000000285b63636363636363630063383730373138383434333300160000000100000005000101010001050f1b171b31323334353637383930313233343536373839303132636363636363636300633231303938373635343332313039383736353433323103060100010511100f1b313233313233313233000000160000000100000007006363636363636363006300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ad";

        foreach (var command in new[]
                 {
                     // wData, rData,
                     "23820000100001003837303030303038323932001600000001000000040063636363636363630063383730303138383334333300a200000064bf4900a82e636363636363636300633837303031383834343333001a000000010000000b00636363636363636300633837303033333737363236001a000000010000000b00636363636363636300633837303034303730303034001a000000010000000b00636363636363636300633837303035333335373030000e000000000000004ce8636363636363636300633837303035383538303230000e00000000000000f40d636363636363636300633837303037333335353030000e00000000000000240e636363636363636300635e",
                     "23820000110001003837303130313331353537000e00000000000000540e636363636363636300633837303130313931393537000e00000000000000840e636363636363636300633837303130343837373537000e00000000000000b40e636363636363636300633837303131313132333030000e00000000000000e40e636363636363636300633837303131313136323031000e00000000000000140f636363636363636300633837303131363631353138000e00000000000000440f636363636363636300633837303131393130373636000e00000000000000740f636363636363636300633837303132303435353035000e00000000000000a40f636363636363636300639f",
                     "23820000120001003837303132313430373730000e00000000000000d40f636363636363636300633837303132313830323635000e000000000000000410636363636363636300633837373835353130373533000e000000000000003410636363636363636300633837303132323030393036000e000000000000006410636363636363636300633837303132323130373738000e000000000000009410636363636363636300633837303132323238353232000e00000000000000c410636363636363636300633837303132323635323437000e00000000000000f410636363636363636300633837303133313734303238000e000000000000002411636363636363636300638c",
                     "23820000130001003837303133313737373934000e000000000000005411636363636363636300633837303133323337333539000e000000000000008411636363636363636300633837303133333038353939000e00000000000000b411636363636363636300633837303133333239383736000e00000000000000f411636363636363636300633837303133333632313434000e000000000000003412636363636363636300633837303133363335383535000e000000000000007412636363636363636300633837303133363837373532000e00000000000000b412636363636363636300633837303133363935373639000e00000000000000f4126363636363636363006308",
                     "23820000140001003837303133363936313936000e000000000000003413636363636363636300633837303134363834343034000e000000000000007413636363636363636300633837303135313331323339000e00000000000000b413636363636363636300633837303135323833303131000e00000000000000f413636363636363636300633837303135333632363236000e000000000000003414636363636363636300633837303135333932393830000e000000000000007414636363636363636300633837303135353031313734000e00000000000000b414636363636363636300633837303135353530303333000e00000000000000f4146363636363636363006395",
                     "23820000150001003837303135353531333835000e000000000000003415636363636363636300633837303135353531333837000e000000000000007415636363636363636300633837303135353536303231000e00000000000000b415636363636363636300633837303135363637383730000e00000000000000f415636363636363636300633837303135373537373430000e000000000000003416636363636363636300633837303135373933333333000e000000000000007416636363636363636300633837303136303635353232000e00000000000000b416636363636363636300633837373838383838373233000e00000000000000f4166363636363636363006399",
                     "23820000160001003837303136353632303938000e000000000000003417636363636363636300633837373134353831353634000e000000000000007417636363636363636300633837303136373130313539000e00000000000000b417636363636363636300633837303137313130373037000e00000000000000f417636363636363636300633837303137313132323232000e000000000000003418636363636363636300633837303137313133303436000e000000000000007418636363636363636300633837303137313135383035000e00000000000000b418636363636363636300633837303137313139343137000e00000000000000f4186363636363636363006393",
                     "23820000170001003837303137313234383338000e000000000000003419636363636363636300633837303137313331373439000e000000000000007419636363636363636300633837303137313336323135000e00000000000000b419636363636363636300633837303137313337333333000e00000000000000f419636363636363636300633837303137313430373134000e00000000000000341a636363636363636300633837303137313434303630000e00000000000000741a636363636363636300633837303137313630323936000e00000000000000b41a636363636363636300633837303137313730323237000e00000000000000f41a6363636363636363006396",
                     "23820000180001003837303137313836323237000e00000000000000341b636363636363636300633837303137313838373336000e00000000000000741b636363636363636300633837303137313838373338000e00000000000000b41b636363636363636300633837303137313932313330000e00000000000000f41b636363636363636300633837303137323232313935000e00000000000000341c636363636363636300633837303137323233313932000e00000000000000741c636363636363636300633837303137323233383530000e00000000000000b41c636363636363636300633837303137323234333932000e00000000000000f41c6363636363636363006391",
                     "23820000190001003837303137323234343834000e00000000000000341d636363636363636300633837303137323236313035000e00000000000000741d636363636363636300633837303137323535363135000e00000000000000b41d636363636363636300633837303137323631373530000e00000000000000f41d636363636363636300633837303137333136303530000e00000000000000341e636363636363636300633837303137333232363431000e00000000000000741e636363636363636300633837303137333331303133000e00000000000000b41e636363636363636300633837303137333331353830000e00000000000000f41e636363636363636300639d",
                     "238200001a0001003837303137333338313032000e00000000000000341f636363636363636300633837303137333536363535000e00000000000000741f636363636363636300633837303137333630393737000e00000000000000b41f636363636363636300633837303137333633373131000e00000000000000f41f636363636363636300633837303137333830363238000e000000000000003420636363636363636300633837303137343430343830000e000000000000007420636363636363636300633837303137343432303033000e00000000000000b420636363636363636300633837303137343435333532000e00000000000000f4206363636363636363006392",
                     "238200001b0001003837303137343438303033000e000000000000003421636363636363636300633837303137343439333838000e000000000000007421636363636363636300633837303137353238363438000e00000000000000b421636363636363636300633837303137353534393639000e00000000000000f421636363636363636300633837303137353835393431000e000000000000003422636363636363636300633837303137353935353735000e000000000000007422636363636363636300633837303137363034383233000e00000000000000b422636363636363636300633837303137363133333830000e00000000000000f4226363636363636363006390",
                     "238200001c0001003837303137363138383434000e000000000000003423636363636363636300633837303137363139393139000e000000000000007423636363636363636300633837303137363635353637000e00000000000000b423636363636363636300633837303137363639373739000e00000000000000f423636363636363636300633837343735333738343535000e000000000000003424636363636363636300633837303137363639393635000e000000000000007424636363636363636300633837303137373530333037000e00000000000000b424636363636363636300633837303137373733343430000e00000000000000f4246363636363636363006398",
                     "238200001d0001003837303137373738363538000e000000000000003425636363636363636300633837303137383032303430000e000000000000007425636363636363636300633837303137383330303132000e00000000000000b425636363636363636300633837303137383830373431000e00000000000000f425636363636363636300633837303137393435373735000e000000000000003426636363636363636300633837373732373030383834000e000000000000007426636363636363636300633837303137393931343337000e00000000000000b426636363636363636300633837303137393931343639000e00000000000000f4266363636363636363006398",
                     "238200001e0001003837303137393939363739000e000000000000003427636363636363636300633837303138303037363735000e000000000000007427636363636363636300633837303138383831303237000e00000000000000b427636363636363636300633837303139323132333330000e00000000000000f427636363636363636300633837303139323132333331000e000000000000003428636363636363636300633837303139343437333232000e000000000000007428636363636363636300633837303139343438323334000e00000000000000b428636363636363636300633837303139353636363939000e00000000000000f428636363636363636300639e",
                     "238200001f0001003837303139393337363139000e000000000000003429636363636363636300633837303230303030303039000e000000000000007429636363636363636300633837303130343536343634000e00000000000000b429636363636363636300633837303231313633303330000e00000000000000f429636363636363636300633837303231323835353031000e00000000000000342a636363636363636300633837303231353135353531000e00000000000000742a636363636363636300633837303130323638383338000e00000000000000b42a636363636363636300633837303231383234353230000e00000000000000f42a6363636363636363006397",
                     "23820000200001003837303231383539373134000e00000000000000342b636363636363636300633837303233333333383236000e00000000000000742b636363636363636300633837303234303038303730000e00000000000000b42b636363636363636300633837303234313830393137000e00000000000000f42b636363636363636300633837303234373030393030000e00000000000000342c636363636363636300633837303234393837373431000e00000000000000742c636363636363636300633837373131303039303239000e00000000000000b42c636363636363636300633837303235363330303331000e00000000000000f42c63636363636363630063ae",
                     "23820000210001003837303236343134323433000e00000000000000342d636363636363636300633837303236343538303435000e00000000000000742d636363636363636300633837303236383838303038000e00000000000000b42d636363636363636300633837303237313433373936000e00000000000000f42d636363636363636300633837303237333930353930000e00000000000000342e636363636363636300633837303237373731313639000e00000000000000742e636363636363636300633837303238303334383133000e00000000000000b42e636363636363636300633837303238343030303035000e00000000000000f42e63636363636363630063a4",
                     "23820000220001003837303531333633383234000e00000000000000342f636363636363636300633837303531383133373832000e00000000000000742f636363636363636300633837303531383930313438000e00000000000000b42f636363636363636300633837303532303138353031001a000000010000000b00636363636363636300633837303532303138353138001a000000010000000b00636363636363636300633837303132323832333233000e0000000000000044cb636363636363636300633837303532343834373138001a000000010000000b00636363636363636300633837303533333337363736001a000000010000000b0063636363636363630063f4",
                     "23820000230001003837303533333338343337000e0000000000000098cb636363636363636300633837303533393930393739001a000000010000000b00636363636363636300633837303535333230323439001a000000010000000b00636363636363636300633837303535353530383230000e00000000000000eccb636363636363636300633837303535353535353733001a000000010000000b00636363636363636300633837303535363939393939001a000000010000000b00636363636363636300633837303536313137363232000e0000000000000040cc636363636363636300633837303538303630303233001a000000010000000b00636363636363636300634e",
                     "23820000240001003837303538383838353939001a000000010000000b00636363636363636300633837303539303033333434000e0000000000000094cc636363636363636300633837303539383936363633001a000000010000000b00636363636363636300633837373135323533333432001a000000010000000b00636363636363636300633837303731313038373437000e00000000000000e8cc6363636363636363006338373037313134343836350016000000d461f4092035636363636363636300633837303731323034373230001a000000010000000b00636363636363636300633837303731323937373237000e000000000000003ccd6363636363636363006363",
                     "23820000250001003837303731383834343333001a000000010000000b00636363636363636300633837303732333334323031001a000000010000000b00636363636363636300633837303733313535343538000e0000000000000090cd636363636363636300633837303733333133333135001a000000010000000b00636363636363636300633837303733333333333639001a000000010000000b00636363636363636300633837303733343039303136000e00000000000000e4cd636363636363636300633837303734303330353535001a000000010000000b00636363636363636300633837303734313732373038001a000000010000000b0063636363636363630063d0",
                     "23820000260001003837303137363639383734000e0000000000000038ce636363636363636300633837303735383038373637001a000000010000000b00636363636363636300633837303735393930383838001a000000010000000b00636363636363636300633837303735393936383237000e000000000000008cce636363636363636300633837303737303030303636001a000000010000000b00636363636363636300633837303737303235353038001a000000010000000b00636363636363636300633837303737323030383939000e00000000000000e0ce636363636363636300633837303737323635333234001a000000010000000b006363636363636363006326",
                     "23820000270001003837303737333537303235001a000000010000000b00636363636363636300633837303737343637313932000e0000000000000034cf636363636363636300633837303737343736303730001a000000010000000b00636363636363636300633837303737353535383430001a000000010000000b00636363636363636300633837303737353934303539000e0000000000000088cf636363636363636300633837303737363638303130001a000000010000000b00636363636363636300633837303738313138383938001a000000010000000b00636363636363636300633837303738313833393736000e00000000000000dccf6363636363636363006319",
                     "23820000280001003837303738323034383535001a000000010000000b00636363636363636300633837303738333136383733001a000000010000000b00636363636363636300633837303738333436363634000e0000000000000030d0636363636363636300633837303738363039323730001a000000010000000b00636363636363636300633837303738393038383838001a000000010000000b00636363636363636300633837303739343933333439000e0000000000000084d0636363636363636300633837303831303434323233001a000000010000000b00636363636363636300633837303831393733383636001a000000010000000b006363636363636363006318",
                     "23820000290001003837303832313235313532000e00000000000000d8d0636363636363636300633837303832313235313631001a000000010000000b00636363636363636300633837303832373038323838001a000000010000000b00636363636363636300633837303834383735363330000e000000000000002cd1636363636363636300633837303835313531383531001a000000010000000b00636363636363636300633837303835333433313831001a000000010000000b00636363636363636300633837303836303436313637000e0000000000000080d1636363636363636300633837303836323833303933001a000000010000000b006363636363636363006310",
                     "238200002a0001003837303837323338303430001a000000010000000b00636363636363636300633837303839373130383836000e00000000000000d4d1636363636363636300633837303839383233363230001a000000010000000b00636363636363636300633837303839383536333737001a000000010000000b00636363636363636300633837343731333636373833000e0000000000000028d2636363636363636300633837343732333638303232001a000000010000000b00636363636363636300633837343733313935383230001a000000010000000b00636363636363636300633837343735393732343033000e000000000000007cd263636363636363630063eb",
                     "238200002b0001003837343736313934363233001a000000010000000b00636363636363636300633837343736373439353437001a000000010000000b00636363636363636300633837343738303735383638000e00000000000000d0d2636363636363636300633837343738343734373338001a000000010000000b00636363636363636300633837373130353632393232001a000000010000000b00636363636363636300633837373132313435303131000e0000000000000024d3636363636363636300633837373133303030303437001a000000010000000b00636363636363636300633837343738303831363730001a000000010000000b00636363636363636300635a",
                     "238200002c0001003837373138303035353134000e0000000000000078d3636363636363636300633837373139303637333231001a000000010000000b00636363636363636300633837373434373737373033001a000000010000000b00636363636363636300633837373530363737373737000e00000000000000ccd3636363636363636300633837373530363937323336001a000000010000000b00636363636363636300633837373530383439373835001a000000010000000b00636363636363636300633837373532373131313131000e0000000000000020d4636363636363636300633837373532393935363131001a000000010000000b0063636363636363630063f6",
                     "238200002d0001003837373533323937323736001a000000010000000b00636363636363636300633837373535343338343838000e0000000000000074d4636363636363636300633837373535343532393432001a000000010000000b00636363636363636300633837373536373332373838001a000000010000000b00636363636363636300633837373537373435353737000e00000000000000c8d4636363636363636300633837303739393333353635001a000000010000000b00636363636363636300633837373538373530363836001a000000010000000b00636363636363636300633837373630303535353531000e000000000000001cd563636363636363630063ce",
                     "238200002e0001003837373630303535353538001a000000010000000b00636363636363636300633837373632303331323432001a000000010000000b00636363636363636300633837373632303736383736000e0000000000000070d5636363636363636300633837373632323030303933001a000000010000000b00636363636363636300633837373632383834363130001a000000010000000b00636363636363636300633837373635313431353933000e00000000000000c4d5636363636363636300633837373635353132393133001a000000010000000b00636363636363636300633837373636363637373739001a000000010000000b00636363636363636300631a",
                     "238200002f0001003837373637373231373737000e0000000000000018d6636363636363636300633837373730303237363034001a000000010000000b00636363636363636300633837373730303534343639001a000000010000000b00636363636363636300633837373730313038313131000e000000000000006cd6636363636363636300633837373730313132323432001a000000010000000b00636363636363636300633837373730313933333330001a000000010000000b00636363636363636300633837373730333335383838000e00000000000000c0d6636363636363636300633837373730363737373737001a000000010000000b0063636363636363630063d1",
                     "23820000300001003837373730383737373130001a000000010000000b00636363636363636300633837373730393935353333000e0000000000000014d7636363636363636300633837373731313137373638001a000000010000000b00636363636363636300633837373731313138323732001a000000010000000b00636363636363636300633837373731313336303030000e0000000000000068d7636363636363636300633837373731313430313131001a000000010000000b00636363636363636300633837373731313731373731001a000000010000000b00636363636363636300633837373731363036393633000e00000000000000bcd763636363636363630063bf",
                     "23820000310001003837373731363231383838001a000000010000000b00636363636363636300633837373732303630303031001a000000010000000b00636363636363636300633837373732313035343537000e0000000000000010d8636363636363636300633837373732313636353135001a000000010000000b00636363636363636300633837373732313639343432001a000000010000000b00636363636363636300633837373732313734343832000e0000000000000064d8636363636363636300633837373732313736323632001a000000010000000b00636363636363636300633837373732323235323233001a000000010000000b0063636363636363630063cd",
                     "23820000320001003837373732323331373230000e00000000000000b8d8636363636363636300633837373732323530323131001a000000010000000b00636363636363636300633837373732323633373036001a000000010000000b00636363636363636300633837373732323737343332000e000000000000000cd9636363636363636300633837373732333636363332001a000000010000000b00636363636363636300633837373732353032343731001a000000010000000b00636363636363636300633837373732353039353034000e0000000000000060d9636363636363636300633837373732353335353535001a000000010000000b0063636363636363630063a8",
                     "23820000330001003837303634313639343432001a000000010000000b00636363636363636300633837373732373032303331000e00000000000000b4d9636363636363636300633837373732373231323536001a000000010000000b00636363636363636300633837373732373830303734001a000000010000000b00636363636363636300633837373732373939363735000e0000000000000008da636363636363636300633837373732383830383834001a000000010000000b00636363636363636300633837373732383837303534001a000000010000000b00636363636363636300633837373733333734313130000e000000000000005cda6363636363636363006395",
                     "23820000340001003837373733343132323238001a000000010000000b00636363636363636300633837373733343532323131001a000000010000000b00636363636363636300633837373733383738333635000e00000000000000b0da636363636363636300633837373733393533323335001a000000010000000b00636363636363636300633837373734343437313730001a000000010000000b00636363636363636300633837373734383139393835000e0000000000000004db636363636363636300633837373734393335313931001a000000010000000b00636363636363636300633837373735303130353232001a000000010000000b006363636363636363006306",
                     "23820000350001003837373735313133353733000e0000000000000058db636363636363636300633837373735373730323134001a000000010000000b00636363636363636300633837373736353735313734001a000000010000000b00636363636363636300633837373736363838383838000e00000000000000acdb636363636363636300633837373736373430303939001a000000010000000b00636363636363636300633837373736383135313538001a000000010000000b00636363636363636300633837373736383434333330000e0000000000000000dc636363636363636300633837373737303532323835001a000000010000000b00636363636363636300638c",
                     "23820000360001003837373737313735303137001a000000010000000b00636363636363636300633837373737323931303837000e0000000000000054dc636363636363636300633837373737383131393636001a000000010000000b00636363636363636300633837373738303230303636001a000000010000000b00636363636363636300633837373738323638383838000e00000000000000a8dc636363636363636300633837303739333231313837001a000000010000000b00636363636363636300633837373739383430343030001a000000010000000b00636363636363636300633837373739393033323332000e00000000000000fcdc6363636363636363006376",
                     "23820000370001003837373831383932333030001a000000010000000b00636363636363636300633837373837323030393039001a000000010000000b00636363636363636300633837373838393532333136000e0000000000000050dd636363636363636300633837303132303836343434001a000000010000000b00636363636363636300633837303137323532393533001a000000010000000b00636363636363636300633837303737343733333333000e00000000000000a4dd636363636363636300633837303137363639383734001a000000010000000b00636363636363636300633837343735333738343535001a000000010000000b006363636363636363006345",
                     "23820000380001003837303133383430333330000e00000000000000f8dd636363636363636300633837343731363939373939001a000000010000000b00636363636363636300633837343734363439333739001a000000010000000b00636363636363636300633837303534383933333638000e000000000000004cde636363636363636300633837303134343136343931001a000000010000000b00636363636363636300633837303131303131323138001a000000010000000b00636363636363636300633837303132383835353730000e00000000000000a0de636363636363636300633837303139383837373033001a000000010000000b006363636363636363006360",
                     "23820000390001003837373736313032363537001a000000010000000b00636363636363636300633837373135323533333431000e00000000000000f4de636363636363636300633837373732383938353032001a000000010000000b00636363636363636300633837373736383136313538001a000000010000000b00636363636363636300633837303131313131363936000e0000000000000048df636363636363636300633837373730323439333134000e000000000000007cdf636363636363636300633837373732323231363533000e00000000000000bcdf636363636363636300633837373731343636393637000e00000000000000fcdf63636363636363630063fc",
                     "238200003a0001003837303830373035373938000e000000000000003ce0636363636363636300633837373735323736383530000e000000000000007ce0636363636363636300633837303131373530323333000e00000000000000bce0636363636363636300633837303733393537393237000e00000000000000fce0636363636363636300633837373539373834363731000e000000000000003ce1636363636363636300633837303839373031303138000e000000000000007ce1636363636363636300633837303535363735383731000e00000000000000bce1636363636363636300633837303734373839383237000e00000000000000fce163636363636363630063b1",
                     "238200003b0001003837343736353839363839000e000000000000003ce2636363636363636300633837303136333236363130000e000000000000007ce2636363636363636300633837373736373736383532000e00000000000000bce26363636363636363006300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000fa",
                 })
        {
            var bytes = command.ToBytesFromHexString();

            var page = bytes[3..5];
            var checksum = bytes.Last();

            var commandShift = page.GetIntFromBytes().GetCommandShift();

            Console.WriteLine(bytes.ToHexString());

            Console.WriteLine($"Command start: {bytes[..8].ToHexString()}");
            // Console.WriteLine($"Checksum: {checksum:X2}");
            // Console.WriteLine($"Command shift: {commandShift:X2}");


            var result = bytes[8..^1].Aggregate<byte, byte>(0, (current, b) => (byte)(current ^ b)) ^ commandShift;

            // Console.WriteLine($"Result: {result :X2}");

            Console.WriteLine(result == checksum ? "Ok" : "Error");


            Console.WriteLine();
        }
    }
}