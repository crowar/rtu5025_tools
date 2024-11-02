using RtuCommon.Extensions;
using RtuCommon.Models.Blocks.Abstracts;
using RtuCommon.Models.Types;

namespace RtuCommon.Models.Blocks;

public sealed class SettingBlock : AbstractBlock
{
    protected override (ushort start, ushort end) Range => (0x0000, 0x0001);


    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DinType Din1Type { get; set; }
    public DinType Din2Type { get; set; }
    public string Din1AlarmContent { get; set; }
    public string Din2AlarmContent { get; set; }
    public string Auto_Arm_after_authorized_number_call_in { get; set; }
    public DisarmArmType Arm_Disarm_after_power_on { get; set; }
    public ControlAuthorization Relay_Control_Authorization { get; set; }
    public string Relay_swtich_ON_timer { get; set; }
    public RelaySwitchType When_relay_switch_ON_by_authorized_number_notify { get; set; }
    public RelaySwitchType When_relay_switch_OFF_by_authorized_number_notify { get; set; }
    public string SMS_content_when_relav_ON { get; set; }
    public string SMS_content_when_relay_OFF { get; set; }
    public string Power_source_failure_alarm_delay { get; set; }
    public string Self_Check_Report_Interval { get; set; }
    public string GPRS_Data_Upload_Interval_Time { get; set; }
    public string Server_IP_Address { get; set; }
    public string Server_Port { get; set; }
    public string GPRS_APN { get; set; }
    public string GPRS_User_Name { get; set; }
    public string GPRS_Password { get; set; }

    public override IEnumerable<byte[]> WriteCommands()
    {
        throw new NotImplementedException();
    }

    public override void Parse(IEnumerable<byte[]> blocks)
    {
        if (blocks == null || !blocks.Any())
        {
            throw new ArgumentNullException(nameof(blocks));
        }

        var bytes = blocks.SelectMany(r => r).ToArray();

        if (bytes == null || !bytes.Any())
        {
            throw new Exception("No data received");
        }

        var field1 = bytes.GetSlice(8, 4);
        var field2 = bytes.GetSlice(field1.LastIndex, 22);
        var field3 = bytes.GetSlice(field2.LastIndex, 1);
        var field4 = bytes.GetSlice(field3.LastIndex, 1);
        var field5 = bytes.GetSlice(field4.LastIndex, 40);
        var field6 = bytes.GetSlice(field5.LastIndex, 40);
        var field7 = bytes.GetSlice(field6.LastIndex, 3);
        var field8 = bytes.GetSlice(field7.LastIndex, 1);
        var field9 = bytes.GetSlice(field8.LastIndex, 1);
        var field10 = bytes.GetSlice(field9.LastIndex, 3);
        var field11 = bytes.GetSlice(field10.LastIndex, 1);
        var field12 = bytes.GetSlice(field11.LastIndex, 1);
        var field13 = bytes.GetSlice(field12.LastIndex, 40);
        var field14 = bytes.GetSlice(field13.LastIndex, 40);
        var field15 = bytes.GetSlice(field14.LastIndex, 3);
        var field16 = bytes.GetSlice(field15.LastIndex, 3);
        var field17 = bytes.GetSlice(field16.LastIndex, 4);
        var field18 = bytes.GetSlice(field17.LastIndex, 32);
        var field19 = bytes.GetSlice(field18.LastIndex, 5);
        var field20 = bytes.GetSlice(field19.LastIndex, 16);
        var field21 = bytes.GetSlice(field20.LastIndex, 16);
        var field22 = bytes.GetSlice(field21.LastIndex, 16);

        Password = System.Text.Encoding.Default.GetString(field1.Result.ExcludeEmpty());
        PhoneNumber = System.Text.Encoding.Default.GetString(field2.Result.ExcludeEmpty());
        Din1Type = field3.Result.ParseDinType();
        Din2Type = field4.Result.ParseDinType();
        Din1AlarmContent = System.Text.Encoding.Default.GetString(field5.Result.ExcludeEmpty());
        Din2AlarmContent = System.Text.Encoding.Default.GetString(field6.Result.ExcludeEmpty());
        Auto_Arm_after_authorized_number_call_in = System.Text.Encoding.Default.GetString(field7.Result.ExcludeEmpty());
        Arm_Disarm_after_power_on = field8.Result.ParseDisarmArmType();
        Relay_Control_Authorization = field9.Result.ParseControlAuthorization();
        Relay_swtich_ON_timer = System.Text.Encoding.Default.GetString(field10.Result.ExcludeEmpty());
        When_relay_switch_ON_by_authorized_number_notify = field11.Result.ParseRelaySwitchType();
        When_relay_switch_OFF_by_authorized_number_notify = field12.Result.ParseRelaySwitchType();
        SMS_content_when_relav_ON = System.Text.Encoding.Default.GetString(field13.Result.ExcludeEmpty());
        SMS_content_when_relay_OFF = System.Text.Encoding.Default.GetString(field14.Result.ExcludeEmpty());
        Power_source_failure_alarm_delay = System.Text.Encoding.Default.GetString(field15.Result.ExcludeEmpty());
        Self_Check_Report_Interval = System.Text.Encoding.Default.GetString(field16.Result.ExcludeEmpty());


        if (field17.Result.ExcludeEmpty().FirstOrDefault() == 0x00)
        {
            GPRS_Data_Upload_Interval_Time = "0";    
        }
        else
        {
            GPRS_Data_Upload_Interval_Time = System.Text.Encoding.Default.GetString(field17.Result.ExcludeEmpty());
        }

        Server_IP_Address = System.Text.Encoding.Default.GetString(field18.Result.ExcludeEmpty());
        Server_Port = System.Text.Encoding.Default.GetString(field19.Result.ExcludeEmpty());

        GPRS_APN = field20.Result.FirstOrDefault() == 0xff ? "" : System.Text.Encoding.Default.GetString(field20.Result.ExcludeEmpty());
        GPRS_User_Name = System.Text.Encoding.Default.GetString(field21.Result.ExcludeEmpty());
        GPRS_Password = System.Text.Encoding.Default.GetString(field22.Result.ExcludeEmpty());


        if (string.IsNullOrEmpty(Power_source_failure_alarm_delay))
            Power_source_failure_alarm_delay = "0";
        if (string.IsNullOrEmpty(Self_Check_Report_Interval))
            Self_Check_Report_Interval = "0";
        if (string.IsNullOrEmpty(GPRS_Data_Upload_Interval_Time))
            GPRS_Data_Upload_Interval_Time = "0";

        if (string.IsNullOrEmpty(Server_Port))
            Server_Port = "0";
    }

    public static SettingBlock Default()
    {
        return new SettingBlock
        {
            Password = "1234",
            PhoneNumber = "",
            Din1Type = DinType.NO,
            Din2Type = DinType.NO,
            Din1AlarmContent = "Unauthorized door opened",
            Din2AlarmContent = "DIN2 Alarm",
            Auto_Arm_after_authorized_number_call_in = "10",
            Arm_Disarm_after_power_on = DisarmArmType.Disarm,
            Relay_Control_Authorization = ControlAuthorization.Authorized,
            Relay_swtich_ON_timer = "0",
            When_relay_switch_ON_by_authorized_number_notify = RelaySwitchType.All,
            When_relay_switch_OFF_by_authorized_number_notify = RelaySwitchType.All,
            SMS_content_when_relav_ON = "Relay ON",
            SMS_content_when_relay_OFF = "Relay OFF",
            Power_source_failure_alarm_delay = "999",
            Self_Check_Report_Interval = "0",
            GPRS_Data_Upload_Interval_Time = "9999",
            Server_IP_Address = "",
            Server_Port = "",
            GPRS_APN = "",
            GPRS_User_Name = "",
            GPRS_Password = ""
        };
    }
}