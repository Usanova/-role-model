   M S B A M L   `   `   `  ����V  RРолевая модель, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Ролевая_модельРолевая_модель  5   	   )    #Ролевая_модель.Desktop   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Ролевая_модель     5   	     � Title$  Desktop��5   	   $	��450��6   $	��800��6&   ��5	      �� 5
   
    �� ImageSource$ Resources/ScreenSaver.jpg��6   6?    � 	Resources 5       �� 
TargetType   ImageButtonStyle �� Property �� Value �� 
TargetType��%�   &	        �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� 5      5      5      5       � ContextMenu 5      �� 5   
   .@�5      m� -   5        $��Создать файл��5      5      5      .��5      �� -   5        $��	spDesktop��5      $	��420��6"   $	��800��6/    �� Orientation$ Vertical=�6;   .+�5   
   � $	��800��6   .+�5      �� $ Vertical=�6   $
��Left=�60   .+�5       �� -   5        $��
tbUserRole��5       $��30��6.   $��	10,10,0,0q�6:   ���	#FFFCF5F5$��14(�6d   Вы вошли как:6�   �� 5!      -   5        $��AdFile��5!      $	��150��6(   $��30��64   $��	10,10,0,0q�6@   $
��Left=�6S   .��6�   Добавить файл6�   5"      �� 5#      $ Vertical=�6   $��Right=�60   .+�5$      �� -   5        $��
EndSession��5$      $	��170��6,   $��30��68   $��	0,10,30,0q�6D   .��6p   !Завершить сессию6�   �� 5%      -   5        $��NotEndSession��5%      $	��170��6/   $��30��6;   $��	0,10,30,0q�6G   .��6v   0.Выйти, не завершая ссесию6�   5&      5'      �� 5(   
   -   5        $��
spForFiles��5(      $	��800��6'   $ Vertical=�63   $��0,10,0,0q�6J   5)      5*      5+      