-- ---------------------------------------------------
-- Get the number of rows in the spool table (backlog)
-- ---------------------------------------------------
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET DEADLOCK_PRIORITY LOW
SELECT  COUNT(*) as Messages,
        'Spooled Messages' as State
FROM [BizTalkMsgboxDb]..[spool] WITH (NOLOCK)
-- ---------------------------------------------------

-- ---------------------------------------------------------
-- Get the number of orchestrations and their state per host
-- ---------------------------------------------------------
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET DEADLOCK_PRIORITY LOW
SELECT  o.nvcName AS Orchestration, COUNT(*) as Count,
        CASE i.nState
                WHEN 1 THEN 'Ready To Run'
                WHEN 2 THEN 'Active'
                WHEN 4 THEN 'Suspended Resumable'
                WHEN 8 THEN 'Dehydrated'
                WHEN 16 THEN 'Completed With Discarded Messages'
                WHEN 32 THEN 'Suspended Non-Resumable'
        END as State
FROM [BizTalkMsgboxDb]..[Instances] AS i WITH (NOLOCK)
JOIN [BizTalkMgmtDb]..[bts_Orchestration] AS o
WITH (NOLOCK) ON i.uidServiceID = o.uidGUID
--WHERE dtCreated > '2004-08-24 00:00:00'
--AND dtCreated < '2004-08-24 13:30:00'
GROUP BY o.nvcName, i.nState
-- ---------------------------------------------------------


-- ----------------------------------------------------------
-- Get the number of Messages per host Q
-- IMPORTANT! Replace each instance of Q with the name of the 
-- host that you are inspecting.
-- and the number of messages in the spool that are in that Q
-- ----------------------------------------------------------
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET DEADLOCK_PRIORITY LOW

SELECT COUNT(*) as Messages, 'Suspended Non-Resumable' as State,
                'BizTalk Server Isolated Host' as Server
FROM [BizTalkMsgBoxDb]..[BizTalkServerIsolatedHostQ_Suspended]
WITH (NOLOCK)
--WHERE dtCreated > '2004-08-24 00:00:00'
--AND dtCreated < '2004-08-24 13:30:00'
SELECT  COUNT(*) as Messages,
        'Spooled Suspended Non-Resumable' as State,
        'BizTalk Server Isolated Host' as Server
FROM [BizTalkMsgboxDb]..[spool] AS i WITH (NOLOCK)
JOIN [BizTalkMsgboxDb]..[BizTalkServerIsolatedHostQ_Suspended] AS o
WITH (NOLOCK) ON i.uidMessageID = o.uidMessageID
--WHERE dtCreated > '2004-08-24 00:00:00'
--AND dtCreated < '2004-08-24 13:30:00'

SELECT COUNT(*) as Messages, 'Scheduled' as State,
        'BizTalk Server Isolated Host' as Server
FROM [BizTalkMsgBoxDb]..[BizTalkServerIsolatedHostQ_Scheduled]
WITH (NOLOCK)
--WHERE dtCreated > '2004-08-24 00:00:00'
--AND dtCreated < '2004-08-24 13:30:00'

SELECT  COUNT(*) as Count, 'Spooled Scheduled' as State,
        'BizTalk Server Isolated Host' as Server
FROM [BizTalkMsgboxDb]..[spool] AS i WITH (NOLOCK)
JOIN [BizTalkMsgboxDb]..[BizTalkServerIsolatedHostQ_Scheduled]
AS o WITH (NOLOCK) ON i.uidMessageID = o.uidMessageID
--WHERE dtCreated > '2004-08-24 00:00:00'
--AND dtCreated < '2004-08-24 13:30:00'

SELECT COUNT(*) as Messages, 'Active' as State,
        'BizTalk Server Isolated Host' as Server
FROM [BizTalkMsgBoxDb]..[BizTalkServerIsolatedHostQ]
WITH (NOLOCK)
--WHERE dtCreated > '2004-08-24 00:00:00'
--AND dtCreated < '2004-08-24 13:30:00'

SELECT  COUNT(*) as Count, 'Spooled Active' as State,
        'BizTalk Server Isolated Host' as Server
FROM [BizTalkMsgboxDb]..[spool] AS i WITH (NOLOCK)
JOIN [BizTalkMsgboxDb]..[BizTalkServerIsolatedHostQ] AS o
WITH (NOLOCK) ON i.uidMessageID = o.uidMessageID
--WHERE dtCreated > '2004-08-24 00:00:00'
--AND dtCreated < '2004-08-24 13:30:00'
-- ----------------------------------------------------------
