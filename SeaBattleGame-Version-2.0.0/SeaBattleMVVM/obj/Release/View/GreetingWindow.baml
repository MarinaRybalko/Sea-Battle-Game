   M S B A M L   `   `   `  ���� L  HLocalizatorHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:LocalizatorHelper;assembly=LocalizatorHelperLocalizatorHelper   SeaBattleMVVM!   SeaBattleMVVM.GeetingWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     XlocalizatorHelper:clr-namespace:LocalizatorHelper;assembly=LocalizatorHelper     -   $��Greeting��  � Title$  Greeting��$	��200��$	��500�� � 
ResizeMode$ NoResize=� � Icon$4 .pack://siteoforigin:,,,/Resources/MainIcon.ico�� � 	Resources *   $LocalizatorHelper.LocalisationHelper 
  Helper��%   &	          .��� 
����·�.+�o� -    o�Name$
 Menu��$��21��$	��Top=�
�����F�$��0,0,388.6,0q�.@�m� $��22��$��0q����� *[MainWindowRes.Language_Btn]+ �� Source# [  .@�m� $��Русский�� m� Command �� *RussianCommand+m� $��English�� �� *EnglishCommand+�� -    ��Name$ TextBoxName��$
��Left=�$��23��$��
130,74,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��209������ 	 �� Path$
	 Name�
 �� Mode$
 TwoWay=��� -    ��Name$ ButtonOk��$��OK��$
��Left=�$��233,128,0,0q�$	��Top=�$��32��$��25������ $	 CloseCommand�����  �� ElementName$ Greeting���� -    ��Name$ 
LabelTitle��$
��Left=�$��
144,31,0,0q�$	��Top=�$��Monotype Corsiva)�$��28(����� *[MainWindowRes.EnterName_Lbl]+# [  �� -   $ 
LabelError��$
��Left=�$��
169,97,0,0q�$	��Top=�$��20(� J� RenderTransformOrigin$ 0.114,0.376&�
���  ��$��Monotype Corsiva)�$y�Hidden=����� *[MainWindowRes.Error_Lbl]+# [  