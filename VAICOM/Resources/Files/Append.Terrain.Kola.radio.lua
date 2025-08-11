dofile('Scripts/World/Radio/ModulationTypes.lua')
dofile('Scripts/World/Radio/FrequencyBands.lua')

local gettext = require("i_18n")
local       _ = gettext.translate

--WORLD RADIO -- Appended by VAICOM

radioTableFormat = 3
radio = {
	{
		radioId = 'airfield4_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("BAS100"), "BAS100"}}};
		frequency = {[UHF] = {MODULATIONTYPE_AM, 257100000.000000}};
		sceneObjects = {'t:5988355'};
	};
	{
		radioId = 'airfield26_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Enontekio"), "Enontekio"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 119700000.000000}};
		sceneObjects = {'t:23514712'};
	};
	{
		radioId = 'EFIV_Ivalo1';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Ivalo"), "Ivalo"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 118000000.000000}};
		sceneObjects = {'t:24093946'};
	};
	{
		radioId = 'airfield18_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Ivalo"), "Ivalo"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 118000000.000000}};
		sceneObjects = {'t:1597440'};
	};
	{
		radioId = 'EFKE_Kemi_Tornio1';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Kemi"), "Kemi"}}};
		frequency = {};
		sceneObjects = {'t:17039766'};
	};
	{
		radioId = 'airfield3_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("KemiTornio"), "KemiTornio"}}};
		frequency = {[UHF] = {MODULATIONTYPE_AM, 250700000.000000},[VHF_HI] = {MODULATIONTYPE_AM, 119400000.000000}};
		sceneObjects = {'t:3631922'};
	};
	{
		radioId = 'airfield16_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Kuusamo"), "Kuusamo"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 118650000.000000}};
		sceneObjects = {'t:18301662'};
	};
	{
		radioId = 'airfield22_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Kittila"), "Kittila"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 118950000.000000}};
		sceneObjects = {'t:1318913'};
	};
	{
		radioId = 'airfield2_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Rovaniemi"), "Rovaniemi"}}};
		frequency = {[UHF] = {MODULATIONTYPE_AM, 250650000.000000},[VHF_HI] = {MODULATIONTYPE_AM, 118700000.000000}};
		sceneObjects = {'t:19801298'};
	};
	{
		radioId = 'airfield27_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Sodankyla"), "Sodankyla"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 123400000.000000}};
		sceneObjects = {'t:234349844'};
	};
	{
		radioId = 'airfield20_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Andoya"), "Andoya"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 118200000.000000}};
		sceneObjects = {'t:26306564'};
	};
	{
		radioId = 'airfield24_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Alta"), "Alta"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 120400000.000000}};
		sceneObjects = {'t:28041920'};
	};
	{
		radioId = 'airfield7_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Bodo"), "Bodo"}}};
		frequency = {[UHF] = {MODULATIONTYPE_AM, 250850000.000000}, [VHF_HI] = {MODULATIONTYPE_AM, 118400000.000000}};
		sceneObjects = {'t:16384'};
	};
	{
		radioId = 'airfield21_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Bardufoss"), "Bardufoss"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 118100000.000000}};
		sceneObjects = {'t:24874811'};
	};
	{
		radioId = 'airfield25_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Evenes"), "Evenes"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 118000000.000000}};
		sceneObjects = {'t:7799459'};
	};
	{
		radioId = 'airfield14_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Kirkenes"), "Kirkenes"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 120350000.000000}};
		sceneObjects = {'t:27995287'};
	};
	{
		radioId = 'airfield1_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Lakselv"), "Lakselv"}}};
		frequency = {[UHF] = {MODULATIONTYPE_AM, 250550000.000000},[VHF_HI] = {MODULATIONTYPE_AM, 118050000.000000}};
		sceneObjects = {'t:10125948'};
	};
	{
		radioId = 'airfield11_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Jokkmokk"), "Jokkmokk"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 123300000.000000}};
		sceneObjects = {'t:4661248'};
	};
	{
		radioId = 'airfield5_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Kiruna"), "Kiruna"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 130150000.000000}};
		sceneObjects = {'t:1277952'};
	};
	{
		radioId = 'airfield15_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Kallax"), "Kallax"}}};
		frequency = {[UHF] = {MODULATIONTYPE_AM, 250300000.000000},[VHF_HI] = {MODULATIONTYPE_AM, 128200000.000000}};
		sceneObjects = {'t:13370198'};
	};
	{
		radioId = 'airfield17_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Vidsel"), "Vidsel"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 119200000.000000}};
		sceneObjects = {'t:16205155'};
	};
	{
		radioId = 'airfield13_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Kalixfors"), "Kalixfors"}}};
		frequency = {[UHF] = {MODULATIONTYPE_AM, 301100000.000000}, [VHF_HI] = {MODULATIONTYPE_AM, 118250000.000000}};
		sceneObjects = {'t:210051124'};
	};
	{
		radioId = 'airfield19_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("alakourtti"), "alakourtti"}}};
		frequency = {};
		sceneObjects = {'t:20996662'};
	};
	{
		radioId = 'airfield8_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Severomorsk-1"), "Severomorsk-1"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 127800000.000000}};
		sceneObjects = {'t:26923537'};
	};
	{
		radioId = 'airfield12_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Murmansk"), "Murmansk"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 127300000.000000}};
		sceneObjects = {'t:1851419'};
	};
	{
		radioId = 'airfield10_0';
		role = {"ground", "tower", "approach"};
		callsign = {};
		frequency = {};
		sceneObjects = {'t:7709254'};
	};
	{
		radioId = 'airfield9_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Olenya"), "Olenya"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 131400000.000000}};
		sceneObjects = {'t:23986517'};
	};
	{
		radioId = 'airfield6_0';
		role = {"ground", "tower", "approach"};
		callsign = {{["common"] = {_("Severomorsk-3"), "Severomorsk-3"}}};
		frequency = {[VHF_HI] = {MODULATIONTYPE_AM, 124300000.000000}};
		sceneObjects = {'t:26610255'};
	};
}