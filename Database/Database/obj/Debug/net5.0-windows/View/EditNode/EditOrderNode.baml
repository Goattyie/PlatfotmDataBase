   M S B A M L   `   `   `  ����C  ?Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Database.View.EditNodeDatabase.View.EditNode  5   	   I)clr-namespace:Database.VeiwModel.EditNodeDatabase.VeiwModel.EditNode  5   H   *    $Database.View.EditNode.EditOrderNode   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Database.View.EditNode     5   	   Bdxg2http://schemas.devexpress.com/winfx/2008/xaml/grid     5   	   >editnode)clr-namespace:Database.VeiwModel.EditNode     6H     � Title$  Заказы��5	   	   $	��599��6   $	��490��6%   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   � $��0��6    5      �� 5      �� 5      .+�5       ��    HeadTextBlock#��[  5!      $��Поставщик��5"      $��Center=�5#      $��Center=�5$      �� 5&      $��1��5'        BaseComboBox#��[ 5(       �� DisplayMemberPath$
 Name��5+      ��5)      �� *DeliverList+ �� SelectedItem 5*      �� *SelectedDeliver+ �� ValidatesOnDataErrors$
 True�� �� ValidatesOnExceptions$
 True�� �� UpdateSourceTrigger$ PropertyChanged=�5+      5,      � 5.   
   $��1��6    5/      �� 50      �� 51      .+�54      �� #��[  55      $��
Товар��56      $��Center=�57      $��Center=�58      �� 5:      $��1��5;      #��[ 5<      $
 Name��5?      ��5=      �� *ProductList+ 5>      �� *SelectedProduct+$
 True��$
 True��$ PropertyChanged=�5?      5@      � 5B   
   $��2��6    5C      �� 5D      �� 5E      .+�5H      �� #��[  5I      $��Количество��5J      $��Center=�5K      $��Center=�5L      �� 5N      $��1��5O        TextBoxWithWatermark#��[ 5P      
 � Tag$ Количество��5Q      ��5R      �� *Count+$
 True��$
 True��$ PropertyChanged=�5S      � 5U   
   $��3��6    5V      �� 5W      �� 5X      .+�5[      �� #��[  5\      $��Получено��5]      $��Center=�5^      $��Center=�5_      �� 5a      $��1��5b      #��[ 5c      $ Получено��5d      ��5e      �� *CurrentCount+$
 True��$
 True��$ PropertyChanged=�5f      � 5h   
   $��4��6    5i      �� 5j      �� 5k      .+�5n      �� #��[  5o      $��Цена закупки��5p      $��Center=�5q      $��Center=�5r      �� 5t      $��1��5u      #��[ 5v      $ Цена закупки��5w      ��5x      �� *	OrderCost+$ PropertyChanged=�5y      � 5{   
   $��5��6    5|      �� 5}      �� 5~      .+�5�      �� #��[  5�      $��Цена доставки��5�      $��Center=�5�      $��Center=�5�      �� 5�      $��1��5�      #��[ 5�      $ Цена доставки��5�      ��5�      �� *DeliverCost+$ PropertyChanged=�5�      � 5�   
   $��6��6    5�      �� 5�      �� 5�      .+�5�      �� #��[  5�      $$��Цена с доставкой��5�      $��Center=�5�      $��Center=�5�      �� 5�      $��1��5�      #��[ 5�      $ Цена оплаты��5�      ��5�      �� *
SummCost+$ PropertyChanged=�5�      � 5�   
   $��7��6    5�      �� 5�      �� 5�      .+�5�      �� #��[  5�      $5��/Оплачено на данный момент��5�      $��Center=�5�      $��Center=�5�      �� 5�      $��1��5�      #��[ 5�      $5 /Оплачено на данный момент��5�      ��5�      �� *CurrentCost+5�      � 5�   
   $��8��6    5�      �� 5�      �� 5�      .+�5�      �� #��[  5�      $��Дата заказа��5�      $��Center=�5�      $��Center=�5�      (  "System.Windows.Controls.DatePicker  5�      $��1��5�         SelectedDate 5�      �� *	OrderDate+5�      �� 5�   
   -   5          
BaseButton#��[ 5�      $��9��5�      $��Сохранить��5�      $��Center=�5�      $��Center=�5�      ��5�      �� *ExecuteCommand+{�5�      �� *	IsValid+6+   5�      5�      