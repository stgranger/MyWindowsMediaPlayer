   M S B A M L   `   `   `  ����C  ?MWidgets, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:MWidgets;assembly=MWidgetsMWidgets  5   	    MyWindowsPlayer%   MyWindowsMediaPlayer.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   =MWidgets(clr-namespace:MWidgets;assembly=MWidgets     5   	   -   5          �Name$  MyMediaPlayer��5   	    � Title$ MyWindowsMediaPlayer��5   	   $��477.4��5   	   $	��530��6    � 
ResizeMode$ NoResize=�66    J� 	AllowDrop .  � Icon$0 *pack://siteoforigin:,,,/Resources/icon.png��5	   1   .��5      � -   5         �Name$ 
WindowGrid��5      $��0,0,0,1q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��139* �6   �� 5      -   5         ��Name$ _ControlsRow��5      $	��73* �62    � ColumnDefinitions 5   
   �� 5      .+�5   
      MWidgets.MMenu  -   5        	  Name$	 _Menu��5      $
��Left=�6(   $��28��6C   $	��Top=�6O   $	��525��6g   $��0,0,-1,0q�6s   .@�5         MWidgets.MMenuItem  -   5        
  Name$
 _MenuItemFile��5   !   $
��File��68   $��22��6F   $��0q�6R   .@�5        -   5        $
 _MenuItemOpen��5   %   $
��Open��6<   6J   �� 5        5      -   5        $
 _MenuItemQuit��5   %   $
��Quit��6<   6J   5         MWidgets.MComboBox  5      -   5          Name$ _ComboBoxLanguage��5   !   $	��162��6<   6H   5         MWidgets.MMediaElement  5   
   -	   5          Name$ _Media��5   "   $��Center=�62   $��Center=�6O    r� LoadedBehavior$ Manual=�6j    r� UnloadedBehavior$ Manual=�6�    J� OpacityMask
 �   �$��1��6     MWidgets.MListBox  5    
   -
   5          Name$ _ListPlayList��5       $
��Left=�63   $��239,35,0,10q�6N   $	��275��6c   $��2��6�      MWidgets.MLabel  5!   
   -   5          Name$ _LabelDuration��5!      $��00:00:00��62   $��456,0,8,111q�6E   $��Center=�6Z   $��Center=�6w   $��60��6�   $��Center=�6�   $��2��6�      MWidgets.MSlider  5"   
   -   5          Name$ _SliderDuration��5"       �� AutoToolTipPlacement$
 None=�64   $��Center=�6P   $��
10,10,73,0q�6m    � Maximum$
 5000Q�6�   $	��Top=�6�   $	��441��6�    J� IsManipulationEnabled .  �� IsMoveToPointEnabled . $��2��6F  $��20��6S    5#   
   -   5        $ _SliderVolume��5#       � Minimum$ 0Q�6U   $ 1Q�6b    � Value$	 100Q�6n   $
��Left=�6z   $��
213,40,0,0q�6�   $	��Top=�6�    �� Orientation$ Vertical=�6�   $��47��6�   $��2��6�      MWidgets.MButton  5$   
   -   5          Name$ _ButtonPlay��5$      $��_Play��60   $
��Left=�6@   $��	70,40,0,0q�6[   $	��Top=�6n   $��80��6�   $��2��6�     5%   
   -   5        $ _ButtonStop��5%      $��_Stop��60   $
��Left=�6@   $��
155,40,0,0q�6[   $	��Top=�6o   $��55��6�   $��Right=�6�   $��2��6�     5&   
   -   5        $ _ButtonPause��5&      $��_Pause��61   $
��Left=�6B   $��4,40,0,0q�6]   $	��Top=�6o   $��61��6�   $
��Left=�6�   $��2��6�     5'   
   -   5        $ _ButtonNext��5'      $
��Next��60   $��Right=�6?   $��
0,67,314,0q�6[   $	��Top=�6o   $��Right=�6�   $��55��6�   $��2��6�     5(   
   -   5        $ _ButtonPrevious��5(      $��Previous��64   $
��Left=�6G   $��4,67,0,0q�6b   $	��Top=�6t    J� RenderTransformOrigin$ 0.365,0.806&�6�   $
��Left=�6�   $��61��6�   $��2��6�     5)   
   -   5        $ _LabelStatus��5)      $��
4,92,288,0q�60   $	��Top=�6D   $��Right=�6\   $��Center=�6   $��28��6�   $��2��6�   5*      5+      