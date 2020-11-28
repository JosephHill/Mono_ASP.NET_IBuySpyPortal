CREATE USER test WITH PASSWORD 'test';
CREATE DATABASE ibuymono WITH OWNER=test ENCODING='UTF8';
\connect ibuymono
CREATE PROCEDURAL LANGUAGE plpgsql;

SET search_path = public, pg_catalog;

--
-- TOC entry 13 (OID 18117)
-- Name: announcements_itemid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE announcements_itemid_seq
    START 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 25 (OID 18119)
-- Name: announcements; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE announcements (
    itemid integer DEFAULT nextval('"announcements_itemid_seq"'::text) NOT NULL,
    moduleid integer NOT NULL,
    createdbyuser character varying(100),
    createddate timestamp without time zone,
    title character varying(150),
    morelink character varying(150),
    mobilemorelink character varying(150),
    expiredate timestamp without time zone,
    description character varying(2000)
);


--
-- TOC entry 14 (OID 18139)
-- Name: contacts_itemid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE contacts_itemid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 26 (OID 18141)
-- Name: contacts; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE contacts (
    itemid integer DEFAULT nextval('"contacts_itemid_seq"'::text) NOT NULL,
    moduleid integer NOT NULL,
    createdbyuser character varying(100),
    createddate timestamp without time zone,
    name character varying(50),
    role character varying(100),
    email character varying(100),
    contact1 character varying(250),
    contact2 character varying(250)
);


--
-- TOC entry 15 (OID 18153)
-- Name: discussion_itemid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE discussion_itemid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 27 (OID 18155)
-- Name: discussion; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE discussion (
    itemid integer DEFAULT nextval('"discussion_itemid_seq"'::text) NOT NULL,
    moduleid integer NOT NULL,
    title character varying(100),
    createddate timestamp without time zone,
    body character varying(3000),
    displayorder character varying(750),
    createdbyuser character varying(100)
);


--
-- TOC entry 16 (OID 18180)
-- Name: documents_itemid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE documents_itemid_seq
    start 18
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 28 (OID 18182)
-- Name: documents; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE documents (
    itemid integer DEFAULT nextval('"documents_itemid_seq"'::text) NOT NULL,
    moduleid integer NOT NULL,
    createdbyuser character varying(100),
    createddate timestamp without time zone,
    filenameurl character varying(250),
    filefriendlyname character varying(150),
    category character varying(50),
    content bytea,
    contenttype character varying(50),
    contentsize integer
);


--
-- TOC entry 17 (OID 18198)
-- Name: events_itemid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE events_itemid_seq
    start 13
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 29 (OID 18200)
-- Name: events; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE events (
    itemid integer DEFAULT nextval('"events_itemid_seq"'::text) NOT NULL,
    moduleid integer NOT NULL,
    createdbyuser character varying(100),
    createddate timestamp without time zone,
    title character varying(150),
    wherewhen character varying(150),
    description character varying(2000),
    expiredate timestamp without time zone
);


--
-- TOC entry 30 (OID 18211)
-- Name: htmltext; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE htmltext (
    moduleid integer NOT NULL,
    desktophtml text NOT NULL,
    mobilesummary text NOT NULL,
    mobiledetails text NOT NULL
);


--
-- TOC entry 18 (OID 18231)
-- Name: links_itemid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE links_itemid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 31 (OID 18233)
-- Name: links; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE links (
    itemid integer DEFAULT nextval('"links_itemid_seq"'::text) NOT NULL,
    moduleid integer NOT NULL,
    createdbyuser character varying(100),
    createddate timestamp without time zone,
    title character varying(100),
    url character varying(250),
    mobileurl character varying(250),
    vieworder integer,
    description character varying(2000)
);


--
-- TOC entry 19 (OID 18254)
-- Name: moduledefinitions_moduledefid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE moduledefinitions_moduledefid_seq
    start 116
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 32 (OID 18256)
-- Name: moduledefinitions; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE moduledefinitions (
    moduledefid integer DEFAULT nextval('"moduledefinitions_moduledefid_seq"'::text) NOT NULL,
    portalid integer NOT NULL,
    friendlyname character varying(128) NOT NULL,
    desktopsrc character varying(256) NOT NULL,
    mobilesrc character varying(256) NOT NULL
);


--
-- TOC entry 20 (OID 18276)
-- Name: modules_moduleid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE modules_moduleid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 33 (OID 18278)
-- Name: modules; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE modules (
    moduleid integer DEFAULT nextval('"modules_moduleid_seq"'::text) NOT NULL,
    tabid integer NOT NULL,
    moduledefid integer NOT NULL,
    moduleorder integer NOT NULL,
    panename character varying(50) NOT NULL,
    moduletitle character varying(256),
    authorizededitroles character varying(256),
    cachetime integer NOT NULL,
    showmobile boolean
);


--
-- TOC entry 34 (OID 18316)
-- Name: modulesettings; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE modulesettings (
    moduleid integer NOT NULL,
    settingname character varying(50) NOT NULL,
    settingvalue character varying(256) NOT NULL
);


--
-- TOC entry 21 (OID 18322)
-- Name: portals_portalid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE portals_portalid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 35 (OID 18324)
-- Name: portals; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE portals (
    portalid integer DEFAULT nextval('"portals_portalid_seq"'::text) NOT NULL,
    portalalias character varying(50),
    portalname character varying(128) NOT NULL,
    alwaysshoweditbutton boolean NOT NULL
);


--
-- TOC entry 22 (OID 18331)
-- Name: roles_roleid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE roles_roleid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 36 (OID 18333)
-- Name: roles; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE roles (
    roleid integer DEFAULT nextval('"roles_roleid_seq"'::text) NOT NULL,
    portalid integer NOT NULL,
    rolename character varying(50) NOT NULL
);


--
-- TOC entry 23 (OID 18339)
-- Name: tabs_tabid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE tabs_tabid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 37 (OID 18341)
-- Name: tabs; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE tabs (
    tabid integer DEFAULT nextval('"tabs_tabid_seq"'::text) NOT NULL,
    taborder integer NOT NULL,
    portalid integer NOT NULL,
    tabname character varying(50) NOT NULL,
    mobiletabname character varying(50) NOT NULL,
    authorizedroles character varying(256),
    showmobile boolean NOT NULL
);


--
-- TOC entry 38 (OID 18353)
-- Name: userroles; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE userroles (
    userid integer NOT NULL,
    roleid integer NOT NULL
);


--
-- TOC entry 24 (OID 18356)
-- Name: users_userid_seq; Type: SEQUENCE; Schema: public; Owner: dev
--

CREATE SEQUENCE users_userid_seq
    start 11
    INCREMENT 1
    MAXVALUE 9223372036854775807
    MINVALUE 1
    CACHE 1;


--
-- TOC entry 39 (OID 18358)
-- Name: users; Type: TABLE; Schema: public; Owner: dev
--

CREATE TABLE users (
    userid integer DEFAULT nextval('"users_userid_seq"'::text) NOT NULL,
    name character varying(50) NOT NULL,
    "password" character varying(20),
    email character varying(100) NOT NULL
);


-- \connect - pg

SET search_path = public, pg_catalog;

--
-- TOC entry 82 (OID 18425)
-- Name: addannouncement (integer, character varying, character varying, character varying, character varying, timestamp without time zone, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addannouncement (integer, character varying, character varying, character varying, character varying, timestamp without time zone, character varying) RETURNS integer
    AS 'INSERT INTO announcements
(
    moduleid,
    createdbyuser,
    createddate,
    title,
    morelink,
    mobilemorelink,
    expiredate,
    description
)

VALUES
(
    $1,
    $2,
    current_timestamp(2),
    $3,
    $4,
    $5,
    $6,
    $7
);

SELECT cast(currval(''announcements_itemid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - dev

SET search_path = public, pg_catalog;

--
-- TOC entry 56 (OID 18426)
-- Name: getlinks (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION getlinks (integer) RETURNS SETOF links
    AS '
select * 
from links 
where moduleid = $1
ORDER BY
  ViewOrder
;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 57 (OID 18430)
-- Name: getannouncements (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION getannouncements (integer) RETURNS SETOF announcements
    AS '
select * 
from announcements 
where moduleid = $1 AND expiredate > current_timestamp;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 59 (OID 18431)
-- Name: getevents (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION getevents (integer) RETURNS SETOF events
    AS '
select * 
from events 
where moduleid = $1 AND expiredate > current_timestamp;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 58 (OID 18432)
-- Name: getmodulesettings (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION getmodulesettings (integer) RETURNS SETOF modulesettings
    AS '
select * 
from modulesettings 
where moduleid = $1;'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - pg

SET search_path = public, pg_catalog;

--
-- TOC entry 60 (OID 18444)
-- Name: gethtmltext (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION gethtmltext (integer) RETURNS SETOF htmltext
    AS '
select * 
from htmltext 
where moduleid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 2 (OID 18456)
-- Name: authroles; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE authroles AS (accessroles character varying(256),
	editroles character varying(256));


--
-- TOC entry 61 (OID 18458)
-- Name: getauthroles (integer, integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getauthroles (integer, integer) RETURNS SETOF authroles
    AS '
SELECT tabs.authorizedroles AS accessroles, 
modules.authorizededitroles AS editroles
FROM modules
INNER JOIN
tabs ON modules.tabid = tabs.tabid

WHERE modules.moduleid = $2
AND tabs.portalid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 62 (OID 18459)
-- Name: getcontacts (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getcontacts (integer) RETURNS SETOF contacts
    AS '
select * 
from contacts 
where moduleid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 63 (OID 18462)
-- Name: getmoduledefinitions (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getmoduledefinitions (integer) RETURNS SETOF moduledefinitions
    AS '
select * 
from moduledefinitions 
where portalid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 64 (OID 18463)
-- Name: getportalroles (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getportalroles (integer) RETURNS SETOF roles
    AS '
select * 
from roles 
where portalid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 3 (OID 18465)
-- Name: rolemembership; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE rolemembership AS (userid integer,
	name character varying(50),
	email character varying(100));


--
-- TOC entry 65 (OID 18467)
-- Name: getrolemembership (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getrolemembership (integer) RETURNS SETOF rolemembership
    AS '
select userroles.userid, name, email 
from userroles 
inner join users on users.userid = userroles.userid
where userroles.roleid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 66 (OID 18468)
-- Name: getrolesbyuser (character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getrolesbyuser (character varying) RETURNS SETOF roles
    AS '
select roles.* 
from userroles 
inner join users on userroles.userid = users.userid
inner join roles on userroles.roleid = roles.roleid
where users.email = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 67 (OID 18469)
-- Name: getsingleannouncement (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsingleannouncement (integer) RETURNS SETOF announcements
    AS '
select * 
from announcements 
where itemid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 68 (OID 18470)
-- Name: getsinglecontact (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsinglecontact (integer) RETURNS SETOF contacts
    AS '
select * 
from contacts 
where itemid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 69 (OID 18471)
-- Name: getsingledocument (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsingledocument (integer) RETURNS SETOF documents
    AS '
select * 
from documents 
where itemid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 70 (OID 18472)
-- Name: getsingleevent (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsingleevent (integer) RETURNS SETOF events
    AS '
select * 
from events 
where itemid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 71 (OID 18473)
-- Name: getsinglelink (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsinglelink (integer) RETURNS SETOF links
    AS '
select * 
from links 
where itemid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 72 (OID 18474)
-- Name: getsinglemoduledefinition (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsinglemoduledefinition (integer) RETURNS SETOF moduledefinitions
    AS '
select * 
from moduledefinitions 
where moduledefid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 73 (OID 18475)
-- Name: getsinglerole (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsinglerole (integer) RETURNS SETOF character varying
    AS '
select rolename
from roles 
where roleid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 4 (OID 18477)
-- Name: singleuser; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE singleuser AS (email character varying(100),
	"password" character varying(20),
	name character varying(50));


--
-- TOC entry 74 (OID 18478)
-- Name: getsingleuser (character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsingleuser (character varying) RETURNS SETOF singleuser
    AS '
select email, password, name
from users 
where email = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 5 (OID 18480)
-- Name: userlist; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE userlist AS (userid integer,
	email character varying(100));


--
-- TOC entry 75 (OID 18481)
-- Name: getusers (); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getusers () RETURNS SETOF userlist
    AS '
select userid, email
from users 
order by email'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - dev

SET search_path = public, pg_catalog;

--
-- TOC entry 76 (OID 18489)
-- Name: gettabs (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION gettabs (integer) RETURNS SETOF tabs
    AS '
    SELECT *
    FROM tabs
    WHERE portalid = $1
    ORDER BY taborder; '
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 6 (OID 18494)
-- Name: modulemoduledefinitions; Type: TYPE; Schema: public; Owner: dev
--

CREATE TYPE modulemoduledefinitions AS (moduleid integer,
	tabid integer,
	moduledefid integer,
	moduleorder integer,
	panename character varying(50),
	moduletitle character varying(256),
	authorizededitroles character varying(256),
	cachetime integer,
	showmobile boolean,
	portalid integer,
	friendlyname character varying(128),
	desktopsrc character varying(256),
	mobilesrc character varying(256));


--
-- TOC entry 77 (OID 18498)
-- Name: getmodulemoduledefinitions (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION getmodulemoduledefinitions (integer) RETURNS SETOF modulemoduledefinitions
    AS '
    SELECT modules.moduleid,
    modules.tabid,
    modules.moduledefid,
    modules.moduleorder,
    modules.panename,
    modules.moduletitle,
    modules.authorizededitroles,
    modules.cachetime,
    modules.showmobile,
    moduledefinitions.portalid,
    moduledefinitions.friendlyname,
    moduledefinitions.desktopsrc,
    moduledefinitions.mobilesrc
    FROM modules
    INNER JOIN moduledefinitions ON modules.moduledefid = moduledefinitions.moduledefid
    WHERE tabid = $1
    ORDER BY moduleorder; '
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 7 (OID 18509)
-- Name: portalsettings; Type: TYPE; Schema: public; Owner: dev
--

CREATE TYPE portalsettings AS (portalid integer,
	portalname character varying(128),
	alwaysshoweditbutton boolean,
	tabid integer,
	tabname character varying(50),
	taborder integer,
	mobiletabname character varying(50),
	authorizedroles character varying(256),
	showmobile boolean);


--
-- TOC entry 78 (OID 18510)
-- Name: getportalsettings (character varying, integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION getportalsettings (character varying, integer) RETURNS SETOF portalsettings
    AS '
    SELECT portals.portalid, portals.portalname, portals.alwaysshoweditbutton, tabs.tabid, tabs.tabname, tabs.taborder, tabs.mobiletabname, tabs.authorizedroles, tabs.showmobile
    FROM tabs
    INNER JOIN portals on tabs.portalid = portals.portalid
    WHERE (0 = $2 AND portals.portalalias = $1) OR (0 != $2 AND tabs.tabid = $2)
    ORDER BY taborder
    LIMIT 1; '
    SECURITY DEFINER LANGUAGE sql;


-- \connect - pg

SET search_path = public, pg_catalog;

--
-- TOC entry 83 (OID 18511)
-- Name: addcontact (integer, character varying, character varying, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addcontact (integer, character varying, character varying, character varying, character varying, character varying, character varying) RETURNS integer
    AS 'INSERT INTO contacts
(
    createdbyuser,
    createddate,
    moduleid,
    name,
    role,
    email,
    contact1,
    contact2
)

VALUES
(
    $2,
    current_timestamp(2),
    $1,
    $3,
    $4,
    $5,
    $6,
    $7
);

SELECT cast(currval(''contacts_itemid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 84 (OID 18512)
-- Name: addevent (integer, character varying, character varying, timestamp without time zone, character varying, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addevent (integer, character varying, character varying, timestamp without time zone, character varying, character varying) RETURNS integer
    AS 'INSERT INTO events
(
    moduleid,
    createdbyuser,
    title,
    createddate,
    expiredate,
    description,
    wherewhen
)

VALUES
(
    $1,
    $2,
    $3,
    current_timestamp(2),
    $4,
    $5,
    $6
);

SELECT cast(currval(''events_itemid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 85 (OID 18513)
-- Name: addlink (integer, character varying, character varying, character varying, character varying, integer, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addlink (integer, character varying, character varying, character varying, character varying, integer, character varying) RETURNS integer
    AS 'INSERT INTO links
(
    moduleid,
    createdbyuser,
    createddate,
    title,
    url,
    mobileurl,
    vieworder,
    description
)

VALUES
(
    $1,
    $2,
    current_timestamp(2),
    $3,
    $4,
    $5,
    $6,
    $7
);

SELECT cast(currval(''links_itemid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 114 (OID 18515)
-- Name: addmodule (integer, integer, character varying, character varying, integer, integer, character varying, boolean); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addmodule (integer, integer, character varying, character varying, integer, integer, character varying, boolean) RETURNS integer
    AS 'INSERT INTO modules
(
    tabid,
    moduleorder,
    moduletitle,
    panename,
    moduledefid,
    cachetime,
    authorizededitroles,
    showmobile
)

VALUES
(
    $1,
    $2,
    $3,
    $4,
    $5,
    $6,
    $7,
    $8
);

SELECT cast(currval(''modules_moduleid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 115 (OID 18516)
-- Name: addmoduledefinition (integer, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addmoduledefinition (integer, character varying, character varying, character varying) RETURNS integer
    AS 'INSERT INTO moduledefinitions
(
    portalid,
    friendlyname,
    desktopsrc,
    mobilesrc
)

VALUES
(
    $1,
    $2,
    $3,
    $4
);

SELECT cast(currval(''moduledefinitions_moduledefid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 116 (OID 18517)
-- Name: addrole (integer, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addrole (integer, character varying) RETURNS integer
    AS 'INSERT INTO roles
(
    portalid,
    rolename
)

VALUES
(
    $1,
    $2
);

SELECT cast(currval(''roles_roleid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 111 (OID 18519)
-- Name: addtab (integer, character varying, integer, character varying, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addtab (integer, character varying, integer, character varying, character varying) RETURNS integer
    AS 'INSERT INTO tabs
(
    portalid,
    tabname,
    taborder,
    showmobile,
    mobiletabname,
    authorizedroles
)

VALUES
(
    $1,
    $2,
    $3,
    false,
    $5,
    $4
);

SELECT cast(currval(''tabs_tabid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - dev

SET search_path = public, pg_catalog;

--
-- TOC entry 110 (OID 18550)
-- Name: adduser (character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION adduser (character varying, character varying, character varying) RETURNS integer
    AS 'INSERT INTO users
(
    name,
    email,
    password
)

VALUES
(
    $1,
    $2,
    $3
);

SELECT cast(currval(''users_userid_seq'') AS int4);'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - pg

SET search_path = public, pg_catalog;

--
-- TOC entry 81 (OID 18563)
-- Name: updatelink (integer, character varying, character varying, character varying, character varying, integer, character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION updatelink (integer, character varying, character varying, character varying, character varying, integer, character varying) RETURNS integer
    AS 'UPDATE links
SET 
    createdbyuser = $2,
    createddate = current_timestamp(2),
    title = $3,
    url = $4,
    mobileurl = $5,
    vieworder = $6,
    description = $7
WHERE itemid = $1;
SELECT 1;
'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - dev

SET search_path = public, pg_catalog;

--
-- TOC entry 80 (OID 18566)
-- Name: updateannouncement (integer, character varying, character varying, character varying, character varying, timestamp without time zone, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updateannouncement (integer, character varying, character varying, character varying, character varying, timestamp without time zone, character varying) RETURNS integer
    AS 'UPDATE announcements
SET 
    createdbyuser = $2,
    createddate = current_timestamp(2),
    title = $3,
    morelink = $4,
    mobilemorelink = $5,
    expiredate = $6,
    description = $7
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 86 (OID 18567)
-- Name: updatecontact (integer, character varying, character varying, character varying, character varying, timestamp without time zone, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatecontact (integer, character varying, character varying, character varying, character varying, timestamp without time zone, character varying) RETURNS integer
    AS 'UPDATE contacts
SET 
    createdbyuser = $2,
    createddate = current_timestamp,
    name = $3,
    role = $4,
    email = $5,
    contact1 = $6,
    contact2 = $7
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 87 (OID 18568)
-- Name: updatecontact (integer, character varying, character varying, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatecontact (integer, character varying, character varying, character varying, character varying, character varying, character varying) RETURNS integer
    AS 'UPDATE contacts
SET 
    createdbyuser = $2,
    createddate = current_timestamp,
    name = $3,
    role = $4,
    email = $5,
    contact1 = $6,
    contact2 = $7
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 117 (OID 18569)
-- Name: updatedocument (integer, integer, character varying, character varying, character varying, character varying, bytea, character varying, integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatedocument (integer, integer, character varying, character varying, character varying, character varying, bytea, character varying, integer) RETURNS integer
    AS '
DECLARE
    _ItemID           ALIAS FOR $1;
    _ModuleID         ALIAS FOR $2;
    _FileFriendlyName ALIAS FOR $3;
    _FileNameUrl      ALIAS FOR $4;
    _UserName         ALIAS FOR $5;
    _Category         ALIAS FOR $6;
    _Content          ALIAS FOR $7;
    _ContentType      ALIAS FOR $8;
    _ContentSize      ALIAS FOR $9;
    t_found           int4;
BEGIN

SELECT 1 INTO t_found FROM documents WHERE itemid = _ItemID;
IF NOT FOUND THEN 
  INSERT INTO documents (
    moduleid,
    filefriendlyname,
    filenameurl,
    createdbyuser,
    createddate,
    category,
    content,
    contenttype,
    contentsize
  )
  VALUES
  (
    _ModuleID,
    _FileFriendlyName,
    _FileNameUrl,
    _UserName,
    _current_timestamp,
    _Category,
    _Content,
    _ContentType,
    _ContentSize
  );
ELSE 
  IF ContentSize = 0 THEN
    UPDATE
      documents
    SET
      createdbyuser    = _UserName,
      createddate      = _current_timestamp,
      category         = _Category,
      filefriendlyname = _FileFriendlyName,
      filenameurl      = _FileNameUrl
    WHERE
      itemid = _ItemID;
  ELSE
    UPDATE
      documents
    SET
      createdbyuser    = _UserName,
      createddate      = _current_timestamp,
      category         = _Category,
      filefriendlyname = _FileFriendlyName,
      filenameurl      = _FileNameUrl,
      content          = _Content,
      contenttype      = _ContentType,
      contentsize      = _ContentSize
    WHERE
      itemid = _ItemID;
  END IF;
END IF;
RETURN 1;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


--
-- TOC entry 88 (OID 18570)
-- Name: updateevent (integer, character varying, character varying, timestamp without time zone, character varying, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updateevent (integer, character varying, character varying, timestamp without time zone, character varying, character varying) RETURNS integer
    AS 'UPDATE events
SET 
    createdbyuser = $2,
    createddate = current_timestamp,
    title = $3,
    expiredate = $4,
    description = $5,
    wherewhen = $6
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 118 (OID 18571)
-- Name: updatehtmltext (integer, text, text, text); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatehtmltext (integer, text, text, text) RETURNS integer
    AS '
DECLARE
    _ModuleID      ALIAS FOR $1;
    _DesktopHtml   ALIAS FOR $2;
    _MobileSummary ALIAS FOR $3;
    _MobileDetails ALIAS FOR $4;
    t_found        int4;
BEGIN

SELECT 1 INTO t_found FROM htmltext WHERE moduleid = _ModuleID;
IF NOT FOUND THEN 
  INSERT INTO htmltext (
    moduleid,
    desktophtml,
    mobilesummary,
    mobiledetails
  ) 
  VALUES (
    _ModuleID,
    _DesktopHtml,
    _MobileSummary,
    _MobileDetails
  );
ELSE 
  UPDATE
    htmltext
  SET
    desktophtml   = _DesktopHtml,
    mobilesummary = _MobileSummary,
    mobiledetails = _MobileDetails
  WHERE
    moduleid = _ModuleID;
END IF;
RETURN 1;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


--
-- TOC entry 89 (OID 18572)
-- Name: updatemodule (integer, integer, character varying, character varying, integer, character varying, boolean); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatemodule (integer, integer, character varying, character varying, integer, character varying, boolean) RETURNS integer
    AS 'UPDATE modules
SET 
    moduleorder = $2,
    moduletitle = $3,
    panename = $4,
    cachetime = $5,
    showmobile = $7,
    authorizededitroles = $6
WHERE moduleid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 90 (OID 18573)
-- Name: updatemoduledefinition (integer, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatemoduledefinition (integer, character varying, character varying, character varying) RETURNS integer
    AS 'UPDATE moduledefinitions
SET 
    friendlyname = $2,
    desktopsrc = $3,
    mobilesrc = $4
WHERE moduledefid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 91 (OID 18574)
-- Name: updatemoduleorder (integer, integer, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatemoduleorder (integer, integer, character varying) RETURNS integer
    AS 'UPDATE modules
SET 
    moduleorder = $2,
    panename = $3
WHERE moduleid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 119 (OID 18575)
-- Name: updatemodulesetting (integer, character varying, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatemodulesetting (integer, character varying, character varying) RETURNS integer
    AS '
DECLARE
    _ModuleID        ALIAS FOR $1;
    _SettingName     ALIAS FOR $2;
    _SettingValue    ALIAS FOR $3;
    t_found          int4;
BEGIN

SELECT 1 INTO t_found FROM modulesettings WHERE moduleid = _ModuleID AND settingname = _SettingName;
IF NOT FOUND THEN 
  INSERT INTO modulesettings (
    moduleid,
    settingname,
    settingvalue
  ) 
  VALUES (
    _ModuleID,
    _SettingName,
    _SettingValue
  );
ELSE 
  UPDATE
    modulesettings
  SET
    settingvalue = _SettingValue
  WHERE
    moduleid = _ModuleID
  AND
    settingname = _SettingName;
END IF;
RETURN 1;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


--
-- TOC entry 92 (OID 18577)
-- Name: updateportalinfo (integer, character varying, boolean); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updateportalinfo (integer, character varying, boolean) RETURNS integer
    AS 'UPDATE portals
SET 
    portalname = $2,
    alwaysshoweditbutton = $3
WHERE portalid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 93 (OID 18579)
-- Name: updaterole (integer, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updaterole (integer, character varying) RETURNS integer
    AS 'UPDATE roles
SET 
    rolename = $2
WHERE roleid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 94 (OID 18581)
-- Name: updatetaborder (integer, integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updatetaborder (integer, integer) RETURNS integer
    AS 'UPDATE tabs
SET 
    taborder = $2
WHERE tabid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 95 (OID 18583)
-- Name: updateuser (integer, character varying, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION updateuser (integer, character varying, character varying) RETURNS integer
    AS 'UPDATE users
SET 
    email = $2,
    password = $3
WHERE userid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 96 (OID 18586)
-- Name: userlogin (character varying, character varying); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION userlogin (character varying, character varying) RETURNS character varying
    AS 'SELECT name FROM users WHERE email = $1 AND password = $2;'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - pg

SET search_path = public, pg_catalog;

--
-- TOC entry 97 (OID 18588)
-- Name: deletelink (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION deletelink (integer) RETURNS integer
    AS 'DELETE FROM links 
WHERE itemid = $1;
SELECT 1'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 8 (OID 18592)
-- Name: documentlist; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE documentlist AS (itemid integer,
	filefriendlyname character varying(150),
	filenameurl character varying(250),
	createdbyuser character varying(100),
	createddate timestamp without time zone,
	category character varying(50),
	contentsize integer);


--
-- TOC entry 98 (OID 18593)
-- Name: getdocuments (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getdocuments (integer) RETURNS SETOF documentlist
    AS '
select itemid,
    filefriendlyname,
    filenameurl,
    createdbyuser,
    createddate,
    category,
    contentsize
from documents 
where moduleid = $1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 9 (OID 18595)
-- Name: document; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE document AS (content bytea,
	contenttype character varying(50),
	contentsize integer,
	filefriendlyname character varying(150));


--
-- TOC entry 99 (OID 18596)
-- Name: getdocumentcontent (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getdocumentcontent (integer) RETURNS SETOF document
    AS '
select content,
    contenttype,
    contentsize,
    filefriendlyname
from documents 
where itemid = $1;'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - dev

SET search_path = public, pg_catalog;

--
-- TOC entry 100 (OID 18597)
-- Name: deleteannouncement (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deleteannouncement (integer) RETURNS integer
    AS 'DELETE FROM announcements 
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 101 (OID 18598)
-- Name: deletecontact (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deletecontact (integer) RETURNS integer
    AS 'DELETE FROM contacts 
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 102 (OID 18600)
-- Name: deletedocument (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deletedocument (integer) RETURNS integer
    AS 'DELETE FROM documents 
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 103 (OID 18601)
-- Name: deleteevent (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deleteevent (integer) RETURNS integer
    AS 'DELETE FROM events 
WHERE itemid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 104 (OID 18602)
-- Name: deletemodule (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deletemodule (integer) RETURNS integer
    AS 'DELETE FROM modules 
WHERE moduleid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 105 (OID 18603)
-- Name: deletemoduledefinition (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deletemoduledefinition (integer) RETURNS integer
    AS 'DELETE FROM moduledefinitions 
WHERE moduledefid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 106 (OID 18604)
-- Name: deleterole (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deleterole (integer) RETURNS integer
    AS 'DELETE FROM roles 
WHERE roleid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 107 (OID 18605)
-- Name: deletetab (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deletetab (integer) RETURNS integer
    AS 'DELETE FROM tabs 
WHERE tabid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 108 (OID 18606)
-- Name: deleteuser (integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deleteuser (integer) RETURNS integer
    AS 'DELETE FROM users 
WHERE userid = $1;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 109 (OID 18607)
-- Name: deleteuserrole (integer, integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION deleteuserrole (integer, integer) RETURNS integer
    AS 'DELETE FROM userroles 
WHERE userid = $1
AND roleid = $2;
SELECT 1;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 112 (OID 18608)
-- Name: adduserrole (integer, integer); Type: FUNCTION; Schema: public; Owner: dev
--

CREATE FUNCTION adduserrole (integer, integer) RETURNS integer
    AS '
DECLARE
  _UserID ALIAS FOR $1;
  _RoleID ALIAS FOR $2;
  t_found int4;
BEGIN

SELECT 1 INTO t_found FROM userroles WHERE userid = _UserID AND roleid = _RoleID;
IF NOT FOUND THEN 
  INSERT INTO userroles
  (
    userid,
    roleid
  )
  VALUES
  (
    _UserID,
    _RoleID
  );
END IF;
RETURN 1;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


-- \connect - pg

SET search_path = public, pg_catalog;

--
-- TOC entry 113 (OID 18621)
-- Name: updatetab (integer, integer, integer, character varying, character varying, character varying, boolean); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION updatetab (integer, integer, integer, character varying, character varying, character varying, boolean) RETURNS integer
    AS '
DECLARE
    _PortalID        ALIAS FOR $1;
    _TabID           ALIAS FOR $2;
    _TabOrder        ALIAS FOR $3;
    _TabName         ALIAS FOR $4;
    _AuthorizedRoles ALIAS FOR $5;
    _MobileTabName   ALIAS FOR $6;
    _ShowMobile      ALIAS FOR $7;
    t_found          int4;
BEGIN
  SELECT 1 INTO t_found FROM tabs WHERE tabid = _TabID;
  IF NOT FOUND THEN 
    INSERT INTO tabs (
      portalid,
      taborder,
      tabname,
      authorizedroles,
      mobiletabname,
      showmobile
    ) 
    VALUES (
      _PortalID,
      _TabOrder,
      _TabName,
      _AuthorizedRoles,
      _MobileTabName,
      _ShowMobile
    );
  ELSE 
    UPDATE
      tabs
    SET
      taborder = _TabOrder,
      tabname = _TabName,
      authorizedroles = _AuthorizedRoles,
      mobiletabname = _MobileTabName,
      showmobile = _ShowMobile
    WHERE
      tabid = _TabID;
  END IF;
  RETURN 1;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


--
-- TOC entry 120 (OID 18625)
-- Name: getnextmessageid (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getnextmessageid (integer) RETURNS integer
    AS '
DECLARE
    _ItemID              ALIAS FOR $1;
    _NextID              int4;
    _CurrentDisplayOrder varchar(750);
    _CurrentModule       int4;
BEGIN

/* Find DisplayOrder of current item */
SELECT INTO _CurrentDisplayOrder,
	    _CurrentModule
                displayorder, 
                moduleid 
  FROM   discussion
  WHERE  itemid = _ItemID;

/* Get the next message in the same module */
SELECT INTO _NextID itemid

FROM
    discussion

WHERE
    displayorder > _CurrentDisplayOrder
    AND
    moduleid = _CurrentModule

ORDER BY
    displayorder ASC
LIMIT 1;

/* end of this thread? */
IF NOT FOUND THEN
    SELECT INTO _NextID NULL;
END IF;
RETURN _NextID;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


--
-- TOC entry 121 (OID 18626)
-- Name: getprevmessageid (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getprevmessageid (integer) RETURNS integer
    AS '
DECLARE
    _ItemID              ALIAS FOR $1;
    _PrevID              int4;
    _CurrentDisplayOrder varchar(750);
    _CurrentModule       int4;
BEGIN

/* Find DisplayOrder of current item */
SELECT INTO _CurrentDisplayOrder,
	    _CurrentModule
                displayorder, 
                moduleid 
  FROM   discussion
  WHERE  itemid = _ItemID;

/* Get the next message in the same module */
SELECT INTO _PrevID itemid

FROM
    discussion

WHERE
    displayorder < _CurrentDisplayOrder
    AND
    moduleid = _CurrentModule

ORDER BY
    displayorder DESC
LIMIT 1;

/* end of this thread? */
IF NOT FOUND THEN
    SELECT INTO _PrevID NULL;
END IF;
RETURN _PrevID;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


--
-- TOC entry 122 (OID 18627)
-- Name: addmessage (character varying, character varying, integer, character varying, integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION addmessage (character varying, character varying, integer, character varying, integer) RETURNS integer
    AS '
DECLARE
    _ItemID             int4;
    _Title              ALIAS FOR $1;
    _Body               ALIAS FOR $2;
    _ParentID           ALIAS FOR $3;
    _UserName           ALIAS FOR $4;
    _ModuleID           ALIAS FOR $5;
    _ParentDisplayOrder varchar(750);
BEGIN

_ParentDisplayOrder := '''';

SELECT INTO
    _ParentDisplayOrder displayorder
FROM 
    discussion 
WHERE 
    itemid = _ParentID;

INSERT INTO discussion
(
    title,
    body,
    displayorder,
    createddate, 
    createdbyuser,
    moduleid
)

VALUES
(
    _Title,
    _Body,
    _ParentDisplayOrder || cast(localtimestamp(3) AS varchar),
    current_timestamp(3),
    _UserName,
    _ModuleID
);

SELECT INTO _ItemID cast(currval(''discussion_itemid_seq'') AS int4);
RETURN _ItemID;
END;'
    SECURITY DEFINER LANGUAGE plpgsql;


--
-- TOC entry 10 (OID 18631)
-- Name: toplevelmessage; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE toplevelmessage AS (itemid integer,
	displayorder character varying(750),
	parent character varying(23),
	childcount integer,
	title character varying(100),
	createdbyuser character varying(100),
	createddate timestamp without time zone);


--
-- TOC entry 123 (OID 18636)
-- Name: gettoplevelmessages (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION gettoplevelmessages (integer) RETURNS SETOF toplevelmessage
    AS '
SELECT
    itemid,
    displayorder,
    CAST(substring(displayorder, 0, 24) AS varchar) AS parent,    
    (SELECT CAST(COUNT(*) - 1 AS int4) 
       FROM discussion disc2 
      WHERE substring(disc2.displayorder, 0, length(TRIM(disc.displayorder))+1) = disc.displayorder) AS childcount,
    title,  
    createdbyuser,
    createddate
FROM 
    discussion disc

WHERE 
    moduleid = $1
  AND
    (length( displayorder ) / 23 ) = 1

ORDER BY
    displayorder;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 11 (OID 18640)
-- Name: threadmessage; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE threadmessage AS (itemid integer,
	displayorder character varying(750),
	indent character varying(750),
	title character varying(100),
	createdbyuser character varying(100),
	createddate timestamp without time zone,
	body character varying(3000));


--
-- TOC entry 124 (OID 18644)
-- Name: getthreadmessages (character varying); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getthreadmessages (character varying) RETURNS SETOF threadmessage
    AS '
SELECT
    itemid,
    displayorder,
    CAST(lpad('''', ((length(displayorder)/23)-1)*30, ''&nbsp;'') AS varchar) AS indent,    
    title,  
    createdbyuser,
    createddate,
    body
FROM 
    discussion

WHERE 
    SUBSTR(displayorder, 1, 23) = $1
  AND
    (length( displayorder ) / 23 ) > 1

ORDER BY
    displayorder;'
    SECURITY DEFINER LANGUAGE sql;


--
-- TOC entry 12 (OID 18646)
-- Name: singlemessage; Type: TYPE; Schema: public; Owner: pg
--

CREATE TYPE singlemessage AS (itemid integer,
	title character varying(100),
	createdbyuser character varying(100),
	createddate timestamp without time zone,
	body character varying(3000),
	displayorder character varying(750),
	nextmessageid integer,
	prevmessageid integer);


--
-- TOC entry 125 (OID 18650)
-- Name: getsinglemessage (integer); Type: FUNCTION; Schema: public; Owner: pg
--

CREATE FUNCTION getsinglemessage (integer) RETURNS SETOF singlemessage
    AS '
SELECT
    itemid,
    title,
    createdbyuser,
    createddate,
    body,
    displayorder,
    getnextmessageid($1) as nextmessageid,
    getprevmessageid($1) as prevmessageid

FROM
    discussion

WHERE
    itemid = $1;'
    SECURITY DEFINER LANGUAGE sql;


-- \connect - dev

SET search_path = public, pg_catalog;

--
-- TOC entry 49 (OID 18321)
-- Name: modulesettings_ix_modulese-0; Type: INDEX; Schema: public; Owner: dev
--

CREATE INDEX "modulesettings_ix_modulese-0" ON modulesettings USING btree (moduleid, settingname);


--
-- TOC entry 53 (OID 18364)
-- Name: users_ix_users_idx; Type: INDEX; Schema: public; Owner: dev
--

CREATE UNIQUE INDEX users_ix_users_idx ON users USING btree (email);


--
-- TOC entry 40 (OID 18125)
-- Name: announcements_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY announcements
    ADD CONSTRAINT announcements_pkey PRIMARY KEY (itemid);


--
-- TOC entry 41 (OID 18144)
-- Name: contacts_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY contacts
    ADD CONSTRAINT contacts_pkey PRIMARY KEY (itemid);


--
-- TOC entry 42 (OID 18161)
-- Name: discussion_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY discussion
    ADD CONSTRAINT discussion_pkey PRIMARY KEY (itemid);


--
-- TOC entry 43 (OID 18188)
-- Name: documents_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY documents
    ADD CONSTRAINT documents_pkey PRIMARY KEY (itemid);


--
-- TOC entry 44 (OID 18206)
-- Name: events_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY events
    ADD CONSTRAINT events_pkey PRIMARY KEY (itemid);


--
-- TOC entry 45 (OID 18216)
-- Name: htmltext_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY htmltext
    ADD CONSTRAINT htmltext_pkey PRIMARY KEY (moduleid);


--
-- TOC entry 46 (OID 18239)
-- Name: links_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY links
    ADD CONSTRAINT links_pkey PRIMARY KEY (itemid);


--
-- TOC entry 47 (OID 18259)
-- Name: moduledefinitions_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY moduledefinitions
    ADD CONSTRAINT moduledefinitions_pkey PRIMARY KEY (moduledefid);


--
-- TOC entry 48 (OID 18281)
-- Name: modules_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY modules
    ADD CONSTRAINT modules_pkey PRIMARY KEY (moduleid);


--
-- TOC entry 50 (OID 18327)
-- Name: portals_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY portals
    ADD CONSTRAINT portals_pkey PRIMARY KEY (portalid);


--
-- TOC entry 51 (OID 18336)
-- Name: roles_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (roleid);


--
-- TOC entry 52 (OID 18344)
-- Name: tabs_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY tabs
    ADD CONSTRAINT tabs_pkey PRIMARY KEY (tabid);


--
-- TOC entry 54 (OID 18361)
-- Name: users_pkey; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY users
    ADD CONSTRAINT users_pkey PRIMARY KEY (userid);


--
-- TOC entry 126 (OID 18365)
-- Name: fk_announcements_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY announcements
    ADD CONSTRAINT fk_announcements_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 127 (OID 18369)
-- Name: fk_contacts_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY contacts
    ADD CONSTRAINT fk_contacts_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 128 (OID 18373)
-- Name: fk_discussion_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY discussion
    ADD CONSTRAINT fk_discussion_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 129 (OID 18377)
-- Name: fk_documents_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY documents
    ADD CONSTRAINT fk_documents_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 130 (OID 18381)
-- Name: fk_events_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY events
    ADD CONSTRAINT fk_events_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 131 (OID 18385)
-- Name: fk_htmltext_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY htmltext
    ADD CONSTRAINT fk_htmltext_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 132 (OID 18389)
-- Name: fk_links_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY links
    ADD CONSTRAINT fk_links_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 133 (OID 18393)
-- Name: fk_modules_moduledefinitions_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY modules
    ADD CONSTRAINT fk_modules_moduledefinitions_fk FOREIGN KEY (moduledefid) REFERENCES moduledefinitions(moduledefid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 134 (OID 18397)
-- Name: fk_modules_tabs_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY modules
    ADD CONSTRAINT fk_modules_tabs_fk FOREIGN KEY (tabid) REFERENCES tabs(tabid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 135 (OID 18401)
-- Name: fk_modulesettings_modules_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY modulesettings
    ADD CONSTRAINT fk_modulesettings_modules_fk FOREIGN KEY (moduleid) REFERENCES modules(moduleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 136 (OID 18405)
-- Name: fk_roles_portals_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY roles
    ADD CONSTRAINT fk_roles_portals_fk FOREIGN KEY (portalid) REFERENCES portals(portalid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 137 (OID 18409)
-- Name: fk_tabs_portals_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY tabs
    ADD CONSTRAINT fk_tabs_portals_fk FOREIGN KEY (portalid) REFERENCES portals(portalid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 138 (OID 18413)
-- Name: fk_userroles_roles_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY userroles
    ADD CONSTRAINT fk_userroles_roles_fk FOREIGN KEY (roleid) REFERENCES roles(roleid) ON UPDATE NO ACTION ON DELETE CASCADE;


--
-- TOC entry 139 (OID 18417)
-- Name: fk_userroles_users_fk; Type: CONSTRAINT; Schema: public; Owner: dev
--

ALTER TABLE ONLY userroles
    ADD CONSTRAINT fk_userroles_users_fk FOREIGN KEY (userid) REFERENCES users(userid) ON UPDATE NO ACTION ON DELETE CASCADE;


