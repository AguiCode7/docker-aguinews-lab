CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261020002704_InitialCreate') THEN
    CREATE TABLE summaries (
        id uuid NOT NULL,
        content text NOT NULL,
        source character varying(200) NOT NULL,
        news_count integer NOT NULL,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_summaries" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261020002704_InitialCreate') THEN
    CREATE INDEX "IX_summaries_created_at" ON summaries (created_at);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261020002704_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261020002704_InitialCreate', '10.0.13');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261021145806_AddArticles') THEN
    CREATE TABLE articles (
        id uuid NOT NULL,
        summary_id uuid NOT NULL,
        position integer NOT NULL,
        title character varying(300) NOT NULL,
        url character varying(2000) NOT NULL,
        source_name character varying(2000) NOT NULL,
        topic character varying(60) NOT NULL,
        headline character varying(400) NOT NULL,
        lead character varying(1000) NOT NULL,
        reading character varying(8000) NOT NULL,
        key_points text[] NOT NULL,
        reading_minutes integer NOT NULL,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_articles" PRIMARY KEY (id),
        CONSTRAINT "FK_articles_summaries_summary_id" FOREIGN KEY (summary_id) REFERENCES summaries (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261021145806_AddArticles') THEN
    CREATE UNIQUE INDEX "IX_articles_summary_id_position" ON articles (summary_id, position);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261021145806_AddArticles') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261021145806_AddArticles', '10.0.13');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261021222656_AddCandidates') THEN
    CREATE TABLE news_candidates (
        id uuid NOT NULL,
        title character varying(300) NOT NULL,
        url character varying(2000) NOT NULL,
        source character varying(100) NOT NULL,
        sources text[] NOT NULL,
        score integer NOT NULL,
        selected boolean NOT NULL,
        published_at timestamp with time zone,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_news_candidates" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261021222656_AddCandidates') THEN
    CREATE INDEX "IX_news_candidates_created_at" ON news_candidates (created_at);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261021222656_AddCandidates') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261021222656_AddCandidates', '10.0.13');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261025182213_AddChannelVideos') THEN
    CREATE TABLE channel_videos (
        id uuid NOT NULL,
        youtube_id character varying(20) NOT NULL,
        title character varying(200) NOT NULL,
        kind character varying(10) NOT NULL,
        news_url character varying(2000),
        published_at timestamp with time zone NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_channel_videos" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261025182213_AddChannelVideos') THEN
    CREATE INDEX "IX_channel_videos_published_at" ON channel_videos (published_at);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261025182213_AddChannelVideos') THEN
    CREATE UNIQUE INDEX "IX_channel_videos_youtube_id" ON channel_videos (youtube_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20261025182213_AddChannelVideos') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261025182213_AddChannelVideos', '10.0.13');
    END IF;
END $EF$;
COMMIT;

