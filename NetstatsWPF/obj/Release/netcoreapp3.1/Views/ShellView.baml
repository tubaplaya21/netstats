   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  D @FluentWPF, Version=0.8.0.6, Culture=neutral, PublicKeyToken=nullP6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPFSourceChord.FluentWPF  Netstats   Netstats.Views.ShellView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     aintrPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ?fw6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPF  -     �Name$  
MainWindow��   MaterialDesignBody#���   $	��780��$	��700�� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������ $	��762��$	��694�� � WindowStartupLocation$ CenterScreen=� � Title �� *Title+���� *WindowHeight+���� *WindowWidth+ � WindowState �� *CurrentWindowState+ � Left �� *LeftPosition+ �� Mode$ OneWayToSource=�
 � Top �� *TopPosition+$ OneWayToSource=�	 � Topmost	 �� *	IsTopMost+$ OneWay=�.   (System.Windows.Interactivity.Interaction
   Triggers
 /   )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.   (System.Windows.Interactivity.TriggerBase   Actions. 6   0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadedWindowCommand+ � 	Resources   VisibleMenu �� 
TargetType �� Triggers �� Binding �� Value �� Property �� Value��%`   &	       �� " o� ��  �� *ShowingMenu+	 False.a���  � y�$ 	Collapsed=� � Triggers :�  :� RoutedEvent$ Window.Loaded�� J� InputBindings �� 
 �� Key$ LeftAlt�� �� 	Modifiers$	 Altf� �� Command �� *ShowMenuCommand+�� $ RightAlt��$	 Altf� �� *ShowMenuCommand+'  !System.Windows.Shell.WindowChrome   WindowChrome      GlassFrameThickness$ 0q�   CornerRadius$ 0��   CaptionHeight �� *CaptionHeight+   ResizeBorderThickness �� *ResizeBorderThickness+.����   �� Padding  �� *ShadowThickness+! �� FallbackValue$! 7��._���   MaterialDesignPaper#���  " �� CornerRadius$" 8��$  0q����� *BorderThickness+# J� Effect# 3  -System.Windows.Media.Effects.DropShadowEffect  $   ShadowDepth$$ 3Q�%   Opacity$% .3Q�._�� & � ColumnDefinitions& �� $��10 ��� $��* ��� $��10 �' � RowDefinitions' �� $��25 ��� $��10������ " ��.���  � ��$ 30 � ��  �� *ShowingMenu+	 False.a���  � ��$ 0 ��� $��50 ��� $	��150���� $��* ��� $	��150���� $��* ��� $	��150���� $��* ��� $	��150���� $��* ��� $��70��$��0.15* ��� $��10 �.+��� $��0��$��0��$��3��  MaterialDesignBackground#���  " �� *Radius+._�]� $��0��$��0��$��3��.+��� $��25��$��25��  HoverlessButton#��[ (   IsHitTestVisibleInChrome$
( True��$��0q�$��5,0,0,0q�+  %MaterialDesignThemes.Wpf.ButtonAssist)   CornerRadius$) 0��+  %MaterialDesignThemes.Wpf.RippleAssist*   RippleSizeMultiplier$	* 500Q�+   
IsCentered$
+ True��, � Cursor$, Arrow��#���   ���� *MenuCommand+.���� $��/heartbeat.ico��$��20��$��20���� - �� Padding$	- 5,5q����� *Title+�� $��Right=�. �� Orientation$. 
Horizontal=�.+��� $��25��$��25��  ControlButton#��[ $
( True��$��0q�$) 0��$	* 500Q�$
+ True��/ � ToolTip$/ Minimize��$, Arrow������ *MinimizeCommand+.���� $��/Minimize.png��$��10��$��10���� $��25��$��25��#��[ $
( True��0   
IsDisabled$
0 True��$) 0��$��0q�$, Arrow��/ �� *MaximizeText+���� *MaximizeCommand+.���� $��10��$��10������ *MaximizeSource+�� $��25��$��25��  WindowCloseButton#��[ $
( True��$��0q�$��Center=�$/ Close��$, Arrow��) �� *ButtonRadius+���� *ExitCommand+.���� $��
/Close.png��$��10��$��10���� $��1��$��1��.+�o� 1 o� 
IsMainMenu$
1 True��$��13(�#���  #��[ $��30��#���   .@�m� $
��File��$��30��.@�m� $��Check for updates��2 m� Command2 �� *CheckUpdateCommand+�� $. 
Horizontal=�.+��� 	Dark Moded� $��10,0,0,0q� * &MaterialDesignSwitchAccentToggleButton#��[ 3 d� 	IsChecked3 �� *IsDarkTheme+m� $��Settings��.@�]� .+��� $
��Left=�Wendy's Datad� $��10,0,0,0q�$��Right=�#��[ 3 �� *
WendysData+]� .+��� $
��Left=�Pizza Hut Datad� $��10,0,0,0q�$��Right=�#��[ 3 �� *	PizzaData+m� $
��Exit��2 �� *ExitCommand+]� $��1��$��2��.+��� $
��Left=�$. 
Horizontal=�.+��� $��15(�$��Store Number:��$��Center=��� $��15(�$��50��$��2q�$��30��$��7,0,0,0q�$	��2,0q� " MaterialDesignFlatButtonRipple.	  (MaterialDesignThemes.Wpf.TextFieldAssist4 	  UnderlineBrush#4 �  5 �� 	MaxLength$5 4��6 �� 
CaretBrush#6 �   ���� *StoreNumber+$ TwoWay=�7 �� UpdateSourceTrigger$7 PropertyChanged=�.�� �� $ Return�� �� *UpdateCommand+.���� -   8 ��Name$8 	btnUpdate�� "	 MaterialDesignFlatDarkBgButton#��[	 $��Update��$��12(�$
��10,2q�$��Center=�9   Feedback9 �#65fffd$) 4������ *UpdateCommand+{��� *IsNotRunning+	� -   : 	�Name$: rdoPizza�� #
 MaterialDesignAccentRadioButton#��[
 $��	Pizza Hut��$��12(�$��Center=�$
3 TrueY�$	��2,0q�	� -   $: 	rdoWendys��#��[
 $��Wendy's��$��12(�$��Center=�$	��2,0q�3 �� *
IsWendys+�� $��Right=�$. 
Horizontal=�.+��� $��
Evan's Box��$��12(�   MaterialDesignAccentCheckBox#��[ $	��8,2q�$��Center=�$��Right=�$
, Hand��3 �� *	IsTopMost+$ OneWayToSource=��� #��[	 $��Doall��$��12(�$	��5,2q�$��Center=�$��Right=�9 �#65fffd$) 4������ *DoallCommand+�� -   $8 btnDameware��#��[	 $��Dameware��$��12(�$	��5,2q�$��Center=�$��Right=�9 �#65fffd$) 4������ *DamewareCommand+�� -   ; ��Name$; grpRegisters��$��1��.
  (MaterialDesignThemes.Wpf.ColorZoneAssist< 
  Mode$< PrimaryDark=�$��3��$��12(�$��4q�$��	Registers��.��� & �� $��5 ��� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��5 �' �� $��0 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+��� -   = ��Name$= lblReg1��$��
Register 1��$��1��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg1+/ �� *RegIp1+�� -   $= lblReg2��$��
Register 2��$��2��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg2+/ �� *RegIp2+�� -	   $= lblReg3��$��
Register 3��$��3��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg3+/ �� *RegIp3+�� -
   $= lblReg4��$��
Register 4��$��4��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg4+/ �� *RegIp4+�� -   $= lblReg5��$��
Register 5��$��5��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg5+/ �� *RegIp5+�� -   $= lblReg6��$��
Register 6��$��6��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg6+/ �� *RegIp6+�� -   $= lblReg7��$��
Register 7��$��7��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg7+/ �� *RegIp7+�� -   $= lblReg8��$��
Register 8��$��8��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg8+/ �� *RegIp8+�� -   $= lblReg9��$��
Register 9��$��9��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *	LabelReg9+/ �� *RegIp9+�� -   $= lblReg10��$��Register 10��$��10��$��1��$��11(�$��5,0,5,0q�$
��Left=����� *
LabelReg10+/ �� *	RegIp10+�� $��50��$��1��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��> �� 
IsReadOnly$
> True��$|�False��$
��Left=�$��25��  MaterialDesignTextBox#��[ ���� *Reg1+/ �� *
InfoReg1+.�� d� ? d� MouseAction$? 	LeftClicke�@ �� CommandParameter$
@ Reg1�� �� *ConnectDeviceCommand+.��A � ContextMenuA ��  .@�m� $��Copy Service Info��B m� CommandParameter$
B Reg1��2 �� *CopyCommand+{��� *InfoEnabledReg1+m� $��Connect to SMB��$
B Reg1��2 �� *
SMBCommand+.���� $��50��$��2��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�C 	  DecorationVisibility$C Hidden=�D �� SelectionBrush
D ���� #��[ $��25������ *Reg2+/ �� *
InfoReg2+.�� d� $? 	LeftClicke�$
@ Reg2�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg2��2 �� *CopyCommand+{��� *InfoEnabledReg2+m� $��Connect to SMB��$
B Reg2��2 �� *
SMBCommand+.���� $��50��$��3��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg3+/ �� *
InfoReg3+.�� d� $? 	LeftClicke�$
@ Reg3�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg3��2 �� *CopyCommand+{��� *InfoEnabledReg3+m� $��Connect to SMB��$
B Reg3��2 �� *
SMBCommand+.���� $��50��$��4��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg4+/ �� *
InfoReg4+.�� d� $? 	LeftClicke�$
@ Reg4�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg4��2 �� *CopyCommand+{��� *InfoEnabledReg4+m� $��Connect to SMB��$
B Reg4��2 �� *
SMBCommand+.���� $��50��$��5��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg5+/ �� *
InfoReg5+.�� d� $? 	LeftClicke�$
@ Reg5�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg5��2 �� *CopyCommand+{��� *InfoEnabledReg5+m� $��Connect to SMB��$
B Reg5��2 �� *
SMBCommand+.���� $��50��$��6��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg6+/ �� *
InfoReg6+.�� d� $? 	LeftClicke�$
@ Reg6�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg6��2 �� *CopyCommand+{��� *InfoEnabledReg6+m� $��Connect to SMB��$
B Reg6��2 �� *
SMBCommand+.���� $��50��$��7��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg7+/ �� *
InfoReg7+.�� d� $? 	LeftClicke�$
@ Reg7�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg7��2 �� *CopyCommand+{��� *InfoEnabledReg7+m� $��Connect to SMB��$
B Reg7��2 �� *
SMBCommand+.���� $��50��$��8��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg8+/ �� *
InfoReg8+.�� d� $? 	LeftClicke�$
@ Reg8�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg8��2 �� *CopyCommand+{��� *InfoEnabledReg8+m� $��Connect to SMB��$
B Reg8��2 �� *
SMBCommand+.���� $��50��$��9��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg9+/ �� *
InfoReg9+.�� d� $? 	LeftClicke�$
@ Reg9�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Reg9��2 �� *CopyCommand+{��� *InfoEnabledReg9+m� $��Connect to SMB��$
B Reg9��2 �� *
SMBCommand+.���� $��50��$��10��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *Reg10+/ �� *	InfoReg10+.�� d� $? 	LeftClicke�$@ Reg10�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$B Reg10��2 �� *CopyCommand+{��� *InfoEnabledReg10+m� $��Connect to SMB��$B Reg10��2 �� *
SMBCommand+.���� -   E ��Name$E 	ReplyReg1��$��1��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��F �� TextAlignment$F Center=�#��[ $��25���� -   $E 	ReplyReg2��$��2��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 	ReplyReg3��$��3��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 	ReplyReg4��$��4��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 	ReplyReg5��$��5��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 	ReplyReg6��$��6��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 	ReplyReg7��$��7��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 	ReplyReg8��$��8��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 	ReplyReg9��$��9��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   $E 
ReplyReg10��$��10��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ $��25���� -   G ��Name$G chkReg1��$��Enable��$��1��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��$
��Reg1��3 �� *CheckedReg1+���� *CheckChangedCommand+�� -   $G chkReg2��$��Enable��$��2��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg2+�� -   $G chkReg3��$��Enable��$��3��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg3+�� -   $G chkReg4��$��Enable��$��4��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg4+�� -   $G chkReg5��$��Enable��$��5��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg5+�� -    $G chkReg6��$��Enable��$��6��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg6+�� -!   $G chkReg7��$��Enable��$��7��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg7+�� -"   $G chkReg8��$��Enable��$��8��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg8+�� -#   $G chkReg9��$��Enable��$��9��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg9+�� -$   $G chkReg10��$��Enable��$��10��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedReg10+�� -%   $; grpKms��$��1��$< PrimaryDark=�$��5��$��12(�$��4q�$	��KMS��.��� & �� $��5 ��� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��5 �' �� $��5 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $��5 �.+��� -&   $= lblKms1��$��1��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms1+/ �� *KmsIp1+�� -'   $= lblKms2��$��2��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms2+/ �� *KmsIp2+�� -(   $= lblKms3��$��3��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms3+/ �� *KmsIp3+�� -)   $= lblKms4��$��4��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms4+/ �� *KmsIp4+�� -*   $= lblKms5��$��5��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms5+/ �� *KmsIp5+�� -+   $= lblKms6��$��6��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms6+/ �� *KmsIp6+�� -,   $= lblKms7��$��7��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms7+/ �� *KmsIp7+�� --   $= lblKms8��$��8��$��1��$��11(�$��5,3,0,3q�$
��Left=����� *	LabelKms8+/ �� *KmsIp8+�� -.   $= lblKms9��$��9��$��1��$��11(�$��5,3,0,3q�$
��Left=�$��2������ *	LabelKms9+/ �� *KmsIp9+�� $��50��$��1��$C Hidden=�$��2��$��11(�$��3,10,3,2q�
�������$��1q�$��25��$
��Left=�$
> True��#��[ ���� *Kms1+, �� *Cursor+/ �� *
InfoKms1+.�� d� $? 	LeftClicke�$
@ Kms1�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms1��2 �� *CopyCommand+{��� *InfoEnabledKms1+m� $��Connect to SMB��$
B Kms1��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��2��$C Hidden=�$��2��$��11(�$��3,10,3,2q�
�������$��1q�$
��Left=�$
> True��#��[ $��25������ *Kms2+, �� *Cursor+/ �� *
InfoKms2+.�� d� $? 	LeftClicke�$
@ Kms2�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms2��2 �� *CopyCommand+{��� *InfoEnabledKms2+m� $��Connect to SMB��$
B Kms2��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��3��$C Hidden=�$��2��$��11(�$��3,10,3,2q�
�������$��1q�$
��Left=�$
> True��#��[ $��25������ *Kms3+, �� *Cursor+/ �� *
InfoKms3+.�� d� $? 	LeftClicke�$
@ Kms3�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms3��2 �� *CopyCommand+{��� *InfoEnabledKms3+m� $��Connect to SMB��$
B Kms3��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��4��$C Hidden=�$��2��$��11(�$��3,10,3,2q�
�������$��1q�$
��Left=�$
> True��#��[ $��25������ *Kms4+, �� *Cursor+/ �� *
InfoKms4+.�� d� $? 	LeftClicke�$
@ Kms4�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms4��2 �� *CopyCommand+{��� *InfoEnabledKms4+m� $��Connect to SMB��$
B Kms4��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��5��$C Hidden=�$��2��$��11(�$��3,10,3,2q�
�������$��1q�$
��Left=�$
> True��#��[ $��25������ *Kms5+, �� *Cursor+/ �� *
InfoKms5+.�� d� $? 	LeftClicke�$
@ Kms5�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms5��2 �� *CopyCommand+{��� *InfoEnabledKms5+m� $��Connect to SMB��$
B Kms5��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��6��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$C Hidden=�$
��Left=�$
> True��#��[ $��25������ *Kms6+, �� *Cursor+/ �� *
InfoKms6+.�� d� $? 	LeftClicke�$
@ Kms6�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms6��2 �� *CopyCommand+{��� *InfoEnabledKms6+m� $��Connect to SMB��$
B Kms6��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��7��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$C Hidden=�$
��Left=�$
> True��#��[ $��25������ *Kms7+, �� *Cursor+/ �� *
InfoKms7+.�� d� $? 	LeftClicke�$
@ Kms7�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms7��2 �� *CopyCommand+{��� *InfoEnabledKms7+m� $��Connect to SMB��$
B Kms7��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��8��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$C Hidden=�$
��Left=�$
> True��#��[ $��25������ *Kms8+, �� *Cursor+/ �� *
InfoKms8+.�� d� $? 	LeftClicke�$
@ Kms8�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms8��2 �� *CopyCommand+{��� *InfoEnabledKms8+m� $��Connect to SMB��$
B Kms8��2 �� *
SMBCommand+{��� *
IsWendys+.���� $��50��$��9��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$C Hidden=�$
��Left=�$
> True��#��[ $��25������ *Kms9+, �� *Cursor+/ �� *
InfoKms9+.�� d� $? 	LeftClicke�$
@ Kms9�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$
B Kms9��2 �� *CopyCommand+{��� *InfoEnabledKms9+m� $��Connect to SMB��$
B Kms9��2 �� *
SMBCommand+{��� *
IsWendys+.���� -/   $E 	ReplyKms1��$��1��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -0   $E 	ReplyKms2��$��2��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -1   $E 	ReplyKms3��$��3��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -2   $E 	ReplyKms4��$��4��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -3   $E 	ReplyKms5��$��5��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -4   $E 	ReplyKms6��$��6��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -5   $E 	ReplyKms7��$��7��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -6   $E 	ReplyKms8��$��8��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -7   $E 	ReplyKms9��$��9��$��3��$��50��$C Hidden=�$��11(�$��
3,3,0,2.63q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$��2��$F Center=�#��[ �� -8   $G CheckedKms1��$��Enable��$��1��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� -9   $G CheckedKms2��$��Enable��$��2��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� -:   $G CheckedKms3��$��Enable��$��3��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� -;   $G CheckedKms4��$��Enable��$��4��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� -<   $G CheckedKms5��$��Enable��$��5��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� -=   $G CheckedKms6��$��Enable��$��6��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� ->   $G CheckedKms7��$��Enable��$��7��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� -?   $G CheckedKms8��$��Enable��$��8��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$
, Hand���� -@   $G CheckedKms9��$��Enable��$��9��#��[ $��4��$��11(�$��2,2.37,0,1.63q�$
��Left=�$��2��$
, Hand���� -A   $; 	grpOffice��$��1��$< PrimaryDark=�$��7��$��12(�$��4q�$��Office��.��� & �� $��5 ��� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��5 �' �� $��5 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $��5 �.+��� -B   $= 
lblOffice1��$	��BKO��$��1��$��1��$��11(�$��5,3,5,3q�$
��Left=�/ �� *	OfficeIp1+�� -C   $= 
lblOffice2��$��Muzak��$��2��$��1��$��11(�$��5,3,5,3q�$
��Left=�/ �� *	OfficeIp2+�� -D   $= 
lblOffice3��$��Printer��$��3��$��1��$��11(�$��5,3,5,3q�$
��Left=�/ �� *	OfficeIp3+�� -E   $= 
lblOffice4��$��BCM50��$��4��$��1��$��11(�$��5,3,5,3q�$
��Left=�/ �� *	OfficeIp4+�� -F   $= 
lblOffice5��$��5��$��1��$��11(�$��5,3,5,3q�$
��Left=����� *LabelOffice5+/ �� *	OfficeIp5+�� -G   $= 
lblOffice6��$
��.253��$��6��$��1��$��11(�$��5,3,5,3q�$
��Left=�/ �� *	OfficeIp6+�� -H   $= 
lblOffice7��$
��.250��$��7��$��1��$��11(�$��5,3,5,3q�$
��Left=�/ �� *	OfficeIp7+�� -I   $= 
lblOffice8��$��8��$��1��$��11(�$��5,3,5,3q�$
��Left=����� *LabelOffice8+/ �� *	OfficeIp8+�� $��50��$��1��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$
, Hand��$
> True��
�������$��1q�#��[ $��25������ *	Office1+/ �� *InfoOffice1+.�� d� $? 	LeftClicke�$@ Office1�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$B Office1��2 �� *CopyCommand+{��� *InfoEnabledOffice1+m� $��Connect to SMB��$B Office1��2 �� *
SMBCommand+.���� $��50��$��2��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$, Arrow��$
> True��
�������$��1q�#��[ $��25������ *	Office2+.�� d� $? 	LeftClicke�$@ Office2�� �� *ConnectDeviceCommand+.���� $��50��$��3��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$
, Hand��$
> True��
�������$��1q�#��[ $��25������ *	Office3+.�� d� $? 	LeftClicke�$@ Office3�� �� *ConnectDeviceCommand+.���� $��50��$��4��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$, Arrow��$
> True��
�������$��1q�#��[ $��25������ *	Office4+.�� d� $? 	LeftClicke�$@ Office4�� �� *ConnectDeviceCommand+.���� $��50��$��5��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$, Arrow��$
> True��
�������$��1q�#��[ $��25������ *	Office5+.�� d� $? 	LeftClicke�$@ Office5�� �� *ConnectDeviceCommand+.���� $��50��$��6��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$, Arrow��$
> True��
�������$��1q�#��[ $��25������ *	Office6+.�� d� $? 	LeftClicke�$@ Office6�� �� *ConnectDeviceCommand+.���� $��50��$��7��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$, Arrow��$
> True��
�������$��1q�#��[ $��25������ *	Office7+.�� d� $? 	LeftClicke�$@ Office7�� �� *ConnectDeviceCommand+.���� $��50��$��8��$C Hidden=�$��2��$��11(�$��3,10,3,2q�$
��Left=�$
, Hand��$
> True��
�������$��1q�#��[ $��25������ *	Office8+/ �� *InfoOffice8+.�� d� $? 	LeftClicke�$@ Office8�� �� *ConnectDeviceCommand+.��A �� .@�m� $��Copy Service Info��$B Office8��2 �� *CopyCommand+{��� *InfoEnabledOffice8+m� $��Connect to SMB��$B Office8��2 �� *
SMBCommand+{��� *
IsPizzaHut+.���� -J   $E ReplyOffice1��$��1��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -K   $E ReplyOffice2��$��2��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -L   $E ReplyOffice3��$��3��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -M   $E ReplyOffice4��$��4��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -N   $E ReplyOffice5��$��5��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -O   $E ReplyOffice6��$��6��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -P   $E ReplyOffice7��$��7��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -Q   $E ReplyOffice8��$��8��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ �� -R   $G CheckedOffice1��$��Enable��$��1��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -S   $G CheckedOffice2��$��Enable��$��2��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -T   $G CheckedOffice3��$��Enable��$��3��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -U   $G CheckedOffice4��$��Enable��$��4��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -V   $G CheckedOffice5��$��Enable��$��5��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -W   $G CheckedOffice6��$��Enable��$��6��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -X   $G CheckedOffice7��$��Enable��$��7��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -Y   $G CheckedOffice8��$��Enable��$��8��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand���� -Z   $; 	grpMobile��$��1��$< PrimaryDark=�$��9��$��12(�$��4q�$��Mobile��.��� & �� $��5 ��� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��60���� $��5 �' �� $��0 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+��� $��Mobile 1��$��1��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp1+�� $��Mobile 2��$��2��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp2+�� $��Mobile 3��$��3��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp3+�� $��Mobile 4��$��4��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp4+�� $��Mobile 5��$��5��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp5+�� $��Mobile 6��$��6��$��1��$��11(�$��5,0,5,0q�/ �� *	MobileIp6+�� $��Mobile 7��$��7��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp7+�� $��Mobile 8��$��8��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp8+�� $��Mobile 9��$��9��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *	MobileIp9+�� $��	Mobile 10��$��10��$��1��$��11(�$��5,0,5,0q�$
��Left=�/ �� *
MobileIp10+�� $��50��$��1��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile1+.�� d� $? 	LeftClicke�$@ Mobile1�� �� *ConnectDeviceCommand+.���� $��50��$��2��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile2+.�� d� $? 	LeftClicke�$@ Mobile2�� �� *ConnectDeviceCommand+.���� $��50��$��3��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile3+.�� d� $? 	LeftClicke�$@ Mobile3�� �� *ConnectDeviceCommand+.���� $��50��$��4��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile4+.�� d� $? 	LeftClicke�$@ Mobile4�� �� *ConnectDeviceCommand+.���� $��50��$��5��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile5+.�� d� $? 	LeftClicke�$@ Mobile5�� �� *ConnectDeviceCommand+.���� $��50��$��6��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile6+.�� d� $? 	LeftClicke�$@ Mobile6�� �� *ConnectDeviceCommand+.���� $��50��$��7��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile7+.�� d� $? 	LeftClicke�$@ Mobile7�� �� *ConnectDeviceCommand+.���� $��50��$��8��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile8+.�� d� $? 	LeftClicke�$@ Mobile8�� �� *ConnectDeviceCommand+.���� $��50��$��9��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *	Mobile9+.�� d� $? 	LeftClicke�$@ Mobile9�� �� *ConnectDeviceCommand+.���� $��50��$��10��
�������$��1q�$��2��$��11(�$��3,10,3,2q�$
, Hand��$
> True��$
��Left=�$C Hidden=�#��[ $��25������ *
Mobile10+.�� d� $? 	LeftClicke�$@ Mobile10�� �� *ConnectDeviceCommand+.���� $��1��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile1+�� $��2��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile2+�� $��3��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile3+�� $��4��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile4+�� $��5��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile5+�� $��6��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile6+�� $��7��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile7+�� $��8��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile8+�� $��9��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile9+�� $��10��$��3��$��50��$C Hidden=�$��11(�$��3q�
�������$��1q�$
��Left=�$, Arrow��$
> True��$F Center=�#��[ ���� *ReplyMobile10+�� $��Enable��$��1��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile1+�� $��Enable��$��2��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile2+�� $��Enable��$��3��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile3+�� $��Enable��$��4��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile4+�� $��Enable��$��5��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile5+�� $��Enable��$��6��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile6+�� $��Enable��$��7��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile7+�� $��Enable��$��8��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile8+�� $��Enable��$��9��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile9+�� $��Enable��$��10��#��[ $��4��$��11(�$��2q�$
��Left=�$
, Hand��3 �� *CheckedMobile10+�� $. 
Horizontal=�$��Right=�$��1��$��11��.+��� -[   $G 
CheckedAll��$��	Check All��#��[ $
��5,10q�$��Center=�$
, Hand���� #��[	 $��Clear��$
��5,20q�$��Center=�$��40��$��12(�9 �#65fffd+  %MaterialDesignThemes.Wpf.ShadowAssistH   ShadowDepth$H Depth0=�$) 4������ *ClearCommand+�� -\   $8 btnRun��#��[	 $	��Run��$
��5,20q�$��Center=�$��40��$��12(�9 �#65fffd$H Depth0=�$) 4������ *
RunCommand+{��� *IsNotRunning+�� -]   $8 btnStop��#��[	 $
��Stop��$
��5,20q�$��Center=�$��40��$��12(�9 �#65fffd$H Depth0=�$) 4������ *StopCommand+{��� *	IsRunning+