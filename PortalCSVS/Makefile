ifndef TARGET
	TARGET=./bin
else
	TARGET=./bin/$(TARGET)
endif

MCS=mcs
ifndef (RELEASE)
	MCSFLAGS=-debug --stacktrace
endif

PORTALCSVS_DLL=$(TARGET)/Portal.dll
PORTALCSVS_PDB=$(TARGET)/Portal.pdb
PORTALCSVS_SRC=Default.aspx.cs \
	DesktopDefault.aspx.cs \
	DesktopModuleTitle.ascx.cs \
	DesktopPortalBanner.ascx.cs \
	Global.asax.cs \
	Admin/AccessDenied.aspx.cs \
	Admin/EditAccessDenied.aspx.cs \
	Admin/Logoff.aspx.cs \
	Admin/ManageUsers.aspx.cs \
	Admin/ModuleDefinitions.aspx.cs \
	Admin/ModuleDefs.ascx.cs \
	Admin/ModuleSettings.aspx.cs \
	Admin/NotImplemented.aspx.cs \
	Admin/Register.aspx.cs \
	Admin/Roles.ascx.cs \
	Admin/SecurityRoles.aspx.cs \
	Admin/SiteSettings.ascx.cs \
	Admin/TabLayout.aspx.cs \
	Admin/Tabs.ascx.cs \
	Admin/Users.ascx.cs \
	Components/AdminDB.cs \
	Components/AnnouncementsDB.cs \
	Components/Configuration.cs \
	Components/ContactsDB.cs \
	Components/DesktopControls.cs \
	Components/DiscussionDB.cs \
	Components/DocumentDB.cs \
	Components/EventsDB.cs \
	Components/HtmlTextDB.cs \
	Components/LinksDB.cs \
	Components/Security.cs \
	DesktopModules/Announcements.ascx.cs \
	DesktopModules/Contacts.ascx.cs \
	DesktopModules/DiscussDetails.aspx.cs \
	DesktopModules/Discussion.ascx.cs \
	DesktopModules/Document.ascx.cs \
	DesktopModules/EditAnnouncements.aspx.cs \
	DesktopModules/EditContacts.aspx.cs \
	DesktopModules/EditDocs.aspx.cs \
	DesktopModules/EditEvents.aspx.cs \
	DesktopModules/EditHtml.aspx.cs \
	DesktopModules/EditImage.aspx.cs \
	DesktopModules/EditLinks.aspx.cs \
	DesktopModules/EditXml.aspx.cs \
	DesktopModules/Events.ascx.cs \
	DesktopModules/HtmlModule.ascx.cs \
	DesktopModules/ImageModule.ascx.cs \
	DesktopModules/Links.ascx.cs \
	DesktopModules/QuickLinks.ascx.cs \
	DesktopModules/SignIn.ascx.cs \
	DesktopModules/ViewDocument.aspx.cs \
	DesktopModules/XmlModule.ascx.cs
PORTALCSVS_RES=-resource:Admin/Register.aspx.resx,ASPNetPortal.Admin.Register.aspx.resx

$(PORTALCSVS_DLL): $(PORTALCSVS_SRC) 
	-mkdir -p $(TARGET)
	$(MCS) $(MCSFLAGS) -r:System.dll -r:System.Drawing.dll -r:System.Data.dll -r:System.Web.dll -r:System.Web.Services.dll -r:System.Xml.dll -r:Npgsql.dll -target:library -out:$(PORTALCSVS_DLL) $(PORTALCSVS_RES) $(PORTALCSVS_SRC)


# common targets

all:	$(PORTALCSVS_DLL)

clean:
	-rm -f "$(PORTALCSVS_DLL)" 2> /dev/null
	-rm -f "$(PORTALCSVS_PDB)" 2> /dev/null


# project names as targets

PortalCSVS: $(PORTALCSVS_DLL)

