   M S B A M L   `   `   `  ����C  ?Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Database.View.Pages.TablesDatabase.View.Pages.Tables  5      C&clr-namespace:Database.VeiwModel.PagesDatabase.VeiwModel.Pages  6>   -    'Database.View.Pages.Tables.Availability   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Database.View.Pages.Tables     5      8pages&clr-namespace:Database.VeiwModel.Pages     6>     N� Title$  Availability��5	       N� 
Background
 ����� � 	Resources 5         Btn �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6   .�5      ��  � ��$ 
10,0,10,10q�6'   6<   �� 5       � ��
 ���� 6A   �� 5       � ��$ 20(�6)   66   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��37* �6   �� 5      $
��377* �6   �� 5      $	��31* �6   .+�5   
   � $��	10,10,0,0q�6    � ColumnDefinitions 5      �� 5      �� 5      �� 5      .+�5      ��   HeadTextBlock#��[ 6     Big#��[ 6?   $��Center=�6_    Таблица: Наличие6�   �� 5       $��1��5!       �� Orientation$ 
Horizontal=�5"      .+�5$      �� #��[ 5%       
 Medium#��[ 5&      $��Center=�5'      $1��+Общая цена с доставкой: ��5(      �� 5*      #��[ 5+      #��[ 5,      $��Center=�5-      ��5.      �� *
DeliverSum+50      �� 52      $��2��53      $ 
Horizontal=�54      .+�56      �� #��[ 57      #��[ 58      $��Center=�59      $(��"Товаров в наличии: ��5:      �� 5<      #��[ 5=      #��[ 5>      $��Center=�5?      ��5@      �� *AvailabilityCount+5B      5C      &   System.Windows.Controls.DataGrid  5D   
   	   AutoGenerateColumns$	 False��5E      
   CanUserAddRows$
 False��5F      $��1��5G      $��10,10,10,10q�5H      
������ ��5J      �� *AvailabilityList+ �� SelectedItem 5K      �� *SelectedAvailability+   Columns 5L      0  *System.Windows.Controls.DataGridTextColumn  5M        HeaderCenterDataGrid,  &System.Windows.Controls.DataGridColumn   HeaderStyle# [ 6%      Header$ 
Товар��6Y      Width5  /System.Windows.Controls.DataGridLengthConverter$	 500 6h   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6t   ��  �� Path$ Product.Name�6�     5N      # [ 6%     CenterGridColumn   	CellStyle# [ 6Y      
FontWeight$
 Bold"�6�     DarkBlueBrush   
Foreground# [ 6�   $ Количество��6�   $	 120 6�    6�   �� $ Count�6    5O      # [ 6%   # [ 6Z   $
 Bold"�6�   # [ 6�   $ Цена продажи��6�   $	 120 6�    6�   �� $ SellCost�6    5P      # [ 6%   # [ 6Z   $ Прибыль��6�   $	 120 6�    6�   �� $ Profit�6�     5Q      # [ 6%   # [ 6Y   $ Цена закупки��6�   $	 120 6�    6�   �� $ BuyCost�6�     5R      # [ 6%   # [ 6Z   $$ Цена с доставкой��6�   $	 120 6�    6�   �� $ DeliverCost�6�     5S      # [ 6%   # [ 6Z   $ Профиль��6�   $	 120 6�    6�   �� $ Profile.Name�6�     5T      # [ 6%   $ Примечание��6Y   $ * 6m    6w   �� $ Comment�6�      RowStyle 5V      �� 5W      )  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5X      �� 5Y       �� Binding 6&   �� $ Count� �� Value 06E   .a�5Z      ��  � ��
 �����6;   5[      5]      5_      � 5a   
   $��2��6    5b      �� 5c      �� 5d      �� 5e      .+�5g      �� $��2��6   $��0��6"   $!��Добавить товар��6g   $��
10,0,10,10q�6�   ��62   ��   
BlueButton �� Source# [ ��6�   �� *
AddCommand+�� 5h      $��2��6   $��1��6"   $-��'Редактировать запись��6g   $��
10,0,10,10q�6�   ��62   �� # [ ��6�   �� *EditCommand+�� 5i      $��2��6   $��2��6"   $!��Удалить запись��6g   $��
10,0,10,10q�6�   ��62   �� # [ ��6�   �� *RemoveCommand+5j      5l      5m      