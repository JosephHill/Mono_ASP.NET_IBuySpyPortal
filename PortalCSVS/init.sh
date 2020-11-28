#!/bin/sh
echo 'echo drop database ibuymono|psql template1' | su postgres
echo 'psql template1 < ddl.sql' | su postgres
echo 'psql ibuymono < PortalData.sql' | su postgres
echo 'psql ibuymono < GrantExec.sql' | su postgres
echo 'psql ibuymono < ResetSequences.sql' | su postgres
echo 'psql ibuymono < AlterTableAndSequenceOwner.sql' | su postgres
echo 'psql ibuymono < AlterFunctionOwner.sql' | su postgres
