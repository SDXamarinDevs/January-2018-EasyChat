namespace EasyChat.Helpers
{
    public static class AppConstants
    {
        public static bool UseAzureFunction => !string.IsNullOrWhiteSpace(AzureFunctionAppName) &&
                                               !string.IsNullOrWhiteSpace(AzureFunctionName) &&
                                               !string.IsNullOrWhiteSpace(AzureFunctionCode);

        public const string AzureFunctionAppName = "";

        public const string AzureFunctionName = "HttpTriggerCSharp1";

        public const string AzureFunctionCode = "";

        // Should be like ws://myapp.azurewebsites.net
        public const string WebSocketHost = "";
    }
}