   M S B A M L   `   `   `  ����L  HLocalizatorHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:LocalizatorHelper;assembly=LocalizatorHelperLocalizatorHelper  5   	    SeaBattleMVVM!   SeaBattleMVVM.GeetingWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   XlocalizatorHelper:clr-namespace:LocalizatorHelper;assembly=LocalizatorHelper     5   	   -   5        $��Greeting��5	   3     � Title$  Greeting��6	   $	��200��6   $	��500��6'    � 
ResizeMode$ NoResize=�6C    � Icon$4 .pack://siteoforigin:,,,/Resources/MainIcon.ico��6Y    � 	Resources 5
      *   $LocalizatorHelper.LocalisationHelper 
  Helper��%   &	          5   
   6@   5      .��5      � 
����·�.+�5   
   o� -   5         o�Name$
 Menu��5      $��21��6   $	��Top=�6)   
�����F�$��0,0,388.6,0q�6X   .@�5      m� $��22��6g   $��0q�6s   ��6   �� *[MainWindowRes.Language_Btn]+ �� Source# [  .@�5      m� $��Русский��6    m� Command 6,   �� *RussianCommand+m� 5      $��English��6    6,   �� *EnglishCommand+5      5      �� 5   
   -   5         ��Name$ TextBoxName��5      $
��Left=�6O   $��23��6j   $��
130,74,0,0q�6v    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��209��6�   ��6'   �� 	 �� Path$
	 Name�
 �� Mode$
 TwoWay=�6�   �� 5   
   -   5         ��Name$ ButtonOk��5      $��OK��6#   $
��Left=�60   $��233,128,0,0q�6K   $	��Top=�6`   $��32��6x   $��25��6�   ��6�   �� $	 CloseCommand���6�   ��  �� ElementName$ Greeting���� 5   
   -   5         ��Name$ 
LabelTitle��5      $
��Left=�6w   $��
144,31,0,0q�6�   $	��Top=�6�   $��Monotype Corsiva)�6�   $��28(�6�   ��6$   �� *[MainWindowRes.EnterName_Lbl]+# [  6�   �� 5   
   -   5        $ 
LabelError��5      $
��Left=�6t   $��
169,97,0,0q�6�   $	��Top=�6�   $��20(�6�    J� RenderTransformOrigin$ 0.114,0.376&�6�   
���  ��$��Monotype Corsiva)�6�   $y�Hidden=�6  ��6$   �� *[MainWindowRes.Error_Lbl]+# [  6  5      5      