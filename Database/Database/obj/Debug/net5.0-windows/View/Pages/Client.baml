   M S B A M L   `   `   `  ����C  ?Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Database.View.Pages.TablesDatabase.View.Pages.Tables  5      C&clr-namespace:Database.VeiwModel.PagesDatabase.VeiwModel.Pages  6>   '    !Database.View.Pages.Tables.Client   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Database.View.Pages.Tables     5      8pages&clr-namespace:Database.VeiwModel.Pages     6>     N� Title$  Client��5	       N� 
Background
 ����� � 	Resources 5         Btn �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6   .�5      ��  � ��$ 
10,0,10,10q�6'   6<   �� 5       � ��
 ���� 6A   �� 5       � ��$ 20(�6)   66   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��37* �6   �� 5      $
��362* �6   �� 5      $	��31* �6   .+�5   
   � $��	10,10,0,0q�6    � ColumnDefinitions 5      �� 5      $
��150* �6#   �� 5      $
��200* �6#   .+�5      �� $��Center=�5        HeadTextBlock#��[ 5        Big#��[ 5        Таблица: Клиенты6C   �� 5!      $��1��5"       �� Orientation$ 
Horizontal=�5#      .+�5$      �� $��Center=�5%        TextBoxWithWatermark#��[ 5&      
	 � Tag$"	 Поиск по номеру��5'      $	��200��5(      ��5)      �� *SearchPhone+�� 5*      $��Center=�5+        
BlueButton#��[ 5,      $��
Найти��5-       	 Small#��[ 5.      ��5/      �� *SearchCommand+50      51      &   System.Windows.Controls.DataGrid  52   
   
   AutoGenerateColumns$
 False��6      CanUserAddRows$ False��53      $��1��54      $��3��55      $��10,10,10,10q�56      
������ ��58      �� *
ClientList+ �� SelectedItem 59      �� *SelectedClient+   Columns 5:      0  *System.Windows.Controls.DataGridTextColumn  5;        HeaderCenterDataGrid,  &System.Windows.Controls.DataGridColumn   HeaderStyle# [ 6%      Header$ Id��6Y      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6e   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6p   ��  �� Path$ Id�6�     5<      # [ 6%   $ Телефон��6Y   $	 100 6j    6v   �� $ Phone�6�     5=      # [ 6%   $ Описание��6Y   $
 300* 6k    6x   �� $ Description�5?      � 5A   
   $��2��6    5B      �� 5C      �� 5D      .+�5F      �� $��2��6   $��0��6"   $%��Добавить клиента��6g   $��
10,0,10,10q�6�   ��62   ��  �� Source# [ ��6�   �� *
AddCommand+�� 5G      $��2��6   $��2��6"   $#��Удалить клиента��6g   $��
10,0,10,10q�6�   ��62   �� # [ ��6�   �� *RemoveCommand+5H      5J      5K      