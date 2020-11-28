select setval('announcements_itemid_seq', (select max(itemid) from announcements))
	where (select max(itemid) from announcements) > 0;
select setval('contacts_itemid_seq', (select max(itemid) from contacts))
	where (select max(itemid) from contacts) > 0;
select setval('discussion_itemid_seq', (select max(itemid) from discussion))
	where (select max(itemid) from discussion) > 0;
select setval('documents_itemid_seq', (select max(itemid) from documents))
	where (select max(itemid) from documents) > 0;
select setval('events_itemid_seq', (select max(itemid) from events))
	where (select max(itemid) from events) > 0;
select setval('links_itemid_seq', (select max(itemid) from links))
	where (select max(itemid) from links) > 0;
select setval('moduledefinitions_moduledefid_seq', (select max(moduledefid) from moduledefinitions))
	where (select max(moduledefid) from moduledefinitions) > 0;
select setval('modules_moduleid_seq', (select max(moduleid) from modules))
	where (select max(moduleid) from modules) > 0;
select setval('portals_portalid_seq', (select max(portalid) from portals))
	where (select max(portalid) from portals) > 0;
select setval('roles_roleid_seq', (select max(roleid) from roles))
	where (select max(roleid) from roles) > 0;
select setval('tabs_tabid_seq', (select max(tabid) from tabs))
	where (select max(tabid) from tabs) > 0;
select setval('users_userid_seq', (select max(userid) from users))
	where (select max(userid) from users) > 0;