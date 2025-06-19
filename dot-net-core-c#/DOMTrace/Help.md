# DOMTrace

## Use of style:

- In your window or user control:
```xml
xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
```
- Then use the styles like:
```xml
<Button Style="{StaticResource PrimaryButton}" Content="Login"/>
<TextBox materialDesign:HintAssist.Hint="Username"/>
<PasswordBox materialDesign:HintAssist.Hint="Password"/>
<CheckBox Content="Remember Me"/>
```