
enum Role
{
    AP_Programmer, CL_Operator, AP_Checker, AP_Header, CL_Header, OP_Operator, OP_Header, VC
}
enum Action
{
    AnyoneRetire,
    ProgrammerAccept, 
    ControllerCompare, 
    CheckerRelease, 
    HeaderRelease,
    ControllerRelease,
    OperatorCommit, 
    OperatorRelease,
    VicehiefRelease,
}


class VersionControlSystem
{
    int[] roles = new int[]
    {
        (int)Role.AP_Programmer,
        (int)Role.AP_Programmer,
        (int)Role.CL_Operator,
        (int)Role.AP_Programmer,
        (int)Role.AP_Checker,
        (int)Role.AP_Header,
        (int)Role.CL_Operator,
        (int)Role.CL_Header,
        (int)Role.OP_Operator,
        (int)Role.OP_Header,
        (int)Role.VC,
    };
}