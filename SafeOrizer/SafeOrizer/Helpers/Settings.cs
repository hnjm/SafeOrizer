// <copyright file="Settings.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters.
  /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string ConnectionStringKey = "constring";
        private static readonly string ConnectionStringDefault = "SafeOrizer";

        public static string GeneralSettings
        {
            get => AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SettingsKey, value);
        }

        public static string ConnectionString
        {
            get => AppSettings.GetValueOrDefault(ConnectionStringKey, ConnectionStringDefault);
            set => AppSettings.AddOrUpdateValue(ConnectionStringKey, value);
        }

    }
}