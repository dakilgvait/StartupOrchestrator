namespace StartupOrchestrator.Abstractions.Enums
{
    public enum PipelineOrderType
    {
        Redirection = 0,
        StaticFiles = 1,
        CookiePolicy = 2,
        Cors = 3,
        Routing = 4,
        Authentication = 5,
        Authorization = 6,
        StatusCodePages = 7,
        Endpoints = 8
    }
}
