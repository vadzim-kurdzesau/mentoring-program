# Description

## Tasks 1 (week 1): 

Create a console application which demonstrates the use of a custom attribute. Attribute should allow to read/write a configuration value via at least two configuration providers: FileConfigurationProvider and ConfigurationManagerConfigurationProvider, which would allow to get/store a setting value in a custom file and app.config (appsettings.json) respectively. It could be a single attribute ConfigurationItemAttribute with parameters: settingName, providerType (File, Configuration Manager); or multiple attributes: FileConfigurationItemAttribute, ConfigurationManagerConfigurationItemAttribute. Any other settings providers are also acceptable, even instead of proposed ones (File / Configuration Manager). 

**Requirements:**

- Created attribute(s) should be applicable only to properties 

- Attributes usage should be implemented in a base class (ConfigurationComponentBase) of a class where the attribute was applied. 

- Attribute should allow to read/write setting values of basic types: int, float, string, TimeSpan  

- Reading / Writing of the settings could be initiated either by a method used in Set / Get parts of the property, or, as a simpler approach, by the methods of the base class (ConfigurationComponentBase): SaveSettings, LoadSettings, which will be invoked externally 

## Task 2 (week 2): 

Required modification of the application from task 1. Move the implementation of the configuration providers (FileConfigurationProvider, ConfigurationManagerConfigurationProvider) into separate assembly files (dll projects) connected to the application as plugins via reflection. Attributes themselves could be left in the application project (for easier usage), but the logic related to the specific settings storage should be moved into separate assemblies. 

## Scoreboard:

1-3 stars – task 1 is completed with different degree of requirements fulfilled.

4 stars – both tasks are implemented with some gaps. 

5 stars – both tasks are completed with all requirements satisfied. 