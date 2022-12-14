
<p align="center">
    <img width=300 src="BEOS-Logo.png"/>
</p>


# BEOS

BEOS (**B**uitenzorg **E**mbedded **O**perating **S**ystem ) adalah OS x64 yang dibuat dengan bahasa C# dan compiler .NET 7 Native AOT technology.

## Building
Buka dengan visual studio 2022, run project BEOS 

### Build requirements
- VMware Workstation Player - https://www.vmware.com/products/workstation-player.html
- Visual studio 2022 - https://visualstudio.microsoft.com/
- QEMU - https://www.qemu.org/download atau VMWare ( Note - USB tidak berfungsi dengan VMWare dan butuh CPU x64, 32 bit tidak di dukung )
- Windows 10-11 x64 or x86
- A CPU minimum keluaran 2012 atau lebih baru, atau dengan tipe Ivy Bridge atau lebih baru
- 4GB RAM tapi 8GB lebih rekomen

<br/>
<hr/>
<br/>

![image](Screenshot.png)

## Features

| Feature | Working in VM | Working on hardware | Information |
| ------- | ------------- | ------------------- | ----------- |
| Applications .mue (BEOS User Executable) | ๐ฉ | ๐ฉ |
| Error Throwing / Catching | ๐ฅ | ๐ฅ | 
| GC | ๐จ | โฌ | Not safe |
| Multiprocessor | ๐ฉ | ๐ฉ |
| Multithreading | ๐ฉ | ๐ฉ |
| PS2 Keyboard/Mouse(USB Compatible) | ๐ฉ | ๐ฉ |
| Nintendo Family Computer Emulator | ๐ฉ | ๐ฉ |
| DOOM(doomgeneric) | ๐ฉ | ๐ฉ |
| Intelยฎ Gigabit Ethernet Network | ๐ฉ | ๐ฉ |
| Realtek RTL8139 | ๐ฉ | โฌ |
| ExFAT | ๐ฉ | ๐ฉ |
| I/O APIC | ๐ฉ | ๐ฉ |
| Local APIC | ๐ฉ | ๐ฉ |
| SATA | ๐ฉ | โฌ |
| IDE | ๐ฉ | ๐ฉ |
| SMBIOS | ๐ฉ | ๐ฉ |
| ACPI | ๐ฉ | ๐ฉ |
| IPv4 | ๐ฉ | ๐ฉ |
| IPv6 | ๐ฅ | ๐ฅ |
| UDP | ๐ฉ | โฌ |
| Lan | ๐ฉ | ๐ฉ |
| Wan | ๐ฉ | ๐ฉ |

| Color Key | Meaning |
| ----- | ------- |
| ๐ฉ | Yes |
| ๐ฅ | No |
| ๐จ | W.I.P / Partially / Buggy |
| โฌ | Unknown |
