   M S B A M L   `   `   `  ����F  BFuMiniHotel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FuMiniHotelFuMiniHotel  5   	        FuMiniHotel.UserManagement   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:FuMiniHotel     5   	     � Title$  UserManagement��5   	   $	��688��6    $	��800��6-   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      $��7* �6   .+�5   
   ]� $��Center=�6   $��5,0,5,0q�6/   $��2��6@   .+�5      � 
����ح� 5      �� 5      $
��261* �6'   �� 5      $
��134* �6'    � RowDefinitions 5      �� 5      $
��Auto �6%   �� 5      $
��Auto �6%   �� 5      $	��180 �6%   �� 5      $��* �6%   .+�5      ��  �� Orientation$ Vertical=�6   $
��Left=�5      $��0, 10, 0, 0q�60   $��2��6E   .+�5      �� -   5         ��Name$ btnBookingReservation��5      $��10q�6<   $��80��6H   $��Booking Reservation��6S   6q   �� 5      -   5        $ btnRoom��5      $��10q�6.   $��80��6:   $
��Room��6E   6T   �� 5!      -   5        $ 
btnBooking��5!      $��10q�61   $��80��6=   $��Booking��6H   6Z   �� 5%      -   5        $ 	btnLogOut��5%      $��10q�60   $��80��6<   $��LogOut��6G   6X   5'      �� 5(      $ Vertical=�6   $
��Left=�5)      $	��500��68   $��2��6D   $��	145,0,0,0q�6X   .+�5,      �� -   5        $��lbTitle��5,      
���  ��$��DemiBold"�6<   $��20(�5-      $��Customer Manager��6*   �� 5/      -   5        $
��lbId��5/      $��
CustomerId��6(   6?   �� 50      -   5        $��txtId��50      $��Stretch=�6+   $��25��51      ��6*   ��  �� Path$ 
CustomerId� �� Mode$ OneWay=� � DataContext 52      ��  �� ElementName$ 	lvMembers��$ SelectedItem��� 54      -   5        $��
lbFullname��54      $��	Full Name��6.   6D   �� 55      -	   5        $��txtFullname��55      $��Stretch=�61   $��25��56      ��6*   �� $ CustomerFullName�$ OneWay=� 57      �� $ 	lvMembers��$ SelectedItem��� 59      -
   5        $��lbEmail��59      $��Email��6+   6=   �� 5:      -   5        $��txtEmail��5:      $��Stretch=�6.   $��25��5;      ��6*   �� $ EmailAddress�$ OneWay=� 5<      �� $ 	lvMembers��$ SelectedItem��� 5>      -   5        $��lbTelephone��5>      $��	Telephone��6/   6E   �� 5?      -   5        $��txtTelephone��5?      $��Stretch=�62   $��25��5@      ��6*   �� $ 	Telephone�$ OneWay=� 5A      �� $ 	lvMembers��$ SelectedItem��� 5C      -   5        $��
lbBirthday��5C      $��Birthday��6.   6C   (  "System.Windows.Controls.DatePicker  5D      -   5        $��txtBirthday��5D   !   $��Stretch=�64   $��25��5E      	   Text	 6*   �� $ CustomerBirthday�$ OneWay=� 5F      �� $ 	lvMembers��$ SelectedItem��� 5H      -   5        $��
lbPassword��5H      $��Password��6.   6C   �� 5I      -   5        $��txtPassword��5I      $��Stretch=�61   $��25��5J      
 �� 
IsReadOnly$

 True��5K   _   ��5J   *   �� $ Password�$ OneWay=� 5K      �� $ 	lvMembers��$ SelectedItem�6_   5L      �� 5N      $ 
Horizontal=�6   $
��Left=�66   $��2��6Q   $��195,342,0,0q�6e   $��2��6z   .+�5O      �� -   5        $ 	btnInsert��5O      $��10q�60   $��80��6<   $��Insert��6G   6X   �� 5P      -   5        $ 	btnUpdate��5P      $��10q�60   $��80��6<   $��Update��6G   6X   �� 5Q      -   5        $ 	btnDelete��5Q      $��10q�60   $��80��6<   $��Delete��6G   6X   �� 5R      -   5        $ 	btnSearch��5R      $��10q�60   $��80��6<   $��Seach��6G   6W   5S      �� 5V      -   5        $��	lvMembers��5V   (   $��1��6   $
��Auto��69   $
��Auto��6F   $��2��6T   $��0,40,0,0q�6h   $��2��6z    �� View 5W      �� 5X      .P�5Y      �� $��
CustomerId��6-    �� Width$	 150��6A    �� DisplayMemberBinding 6M   �� $ 
CustomerId��� 5Z      $��Email��6-   $	 150��6<    6H   �� $ EmailAddress��� 5[      $��	Full Name��6-   $	 150��6@    6L   �� $ CustomerFullName��� 5\      $��	Telephone��6-   $	 150��6@    6L   �� $ 	Telephone��� 5]      $��Birthday��6-   $	 150��6?    6K   �� $ CustomerBirthday��� 5^      $��Password��6-   $	 100��6?    6K   �� $ Password�5_      5b      5f      5g      5h      5i      