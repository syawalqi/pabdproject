PnajnCREATE DATABASE MANDAK;

CREATE TABLE Karyawan (
    ID_Karyawan INT IDENTITY(1,1) PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Jabatan VARCHAR(50) NOT NULL,
    Departemen VARCHAR(50) NOT NULL,
    Tanggal_Masuk DATE NOT NULL
);

select * from Karyawan
INSERT INTO Karyawan (Nama, Jabatan, Departemen, Tanggal_Masuk)
VALUES ('Admin', 'Staff', 'Manusia', '2024-01-15');

INSERT INTO Karyawan (Nama, Jabatan, Departemen, Tanggal_Masuk, Role)
VALUES ('Admin', 'Staff', 'Manusia', '2024-01-15', 'admin');


ALTER TABLE Karyawan
ADD Passwd VARCHAR(255) NOT NULL DEFAULT 'defaultPass';

ALTER TABLE Karyawan ADD Role VARCHAR(20) NOT NULL DEFAULT 'employee';


CREATE TABLE Kehadiran (
    ID_Kehadiran INT IDENTITY(1,1) PRIMARY KEY,
    ID_Karyawan INT NOT NULL CHECK (ID_Karyawan > 0),
    Tanggal DATE NOT NULL,
    Waktu_Masuk DATETIME2 NULL,
    Waktu_Keluar DATETIME2 NULL,
    Status VARCHAR(20) CHECK (Status IN ('Hadir', 'Izin', 'Sakit', 'Alpha')) NOT NULL,
    FOREIGN KEY (ID_Karyawan) REFERENCES Karyawan(ID_Karyawan) ON DELETE CASCADE
);
select * from Kehadiran
ALTER TABLE Kehadiran
    ALTER COLUMN Waktu_Masuk DATETIME2 NULL;

ALTER TABLE Kehadiran
    ALTER COLUMN Waktu_Keluar DATETIME2 NULL;

CREATE TABLE Cuti (
    ID_Cuti INT IDENTITY(1,1) PRIMARY KEY,
    ID_Karyawan INT NOT NULL,
    Tanggal_Pengajuan DATETIME DEFAULT GETDATE(),
    Tanggal_Mulai DATE NOT NULL,
    Tanggal_Selesai DATE NOT NULL,
    Jenis_Cuti VARCHAR(50) NOT NULL, -- Contoh: Tahunan, Sakit, Melahirkan, Menikah, dll.
    Keterangan_Cuti NVARCHAR(500) NULL, -- Alasan pengajuan cuti dari karyawan
    Status_Persetujuan VARCHAR(20) DEFAULT 'Menunggu' CHECK (Status_Persetujuan IN ('Menunggu', 'Disetujui', 'Ditolak')),
    ID_Approver INT NULL, -- Karyawan yang menyetujui/menolak (ID_Karyawan dari Karyawan yang ber-role admin/hrd)
    Tanggal_Approval DATETIME NULL,
    Keterangan_Approval NVARCHAR(500) NULL, -- Alasan persetujuan/penolakan dari admin/HRD
    FOREIGN KEY (ID_Karyawan) REFERENCES Karyawan(ID_Karyawan),
    FOREIGN KEY (ID_Approver) REFERENCES Karyawan(ID_Karyawan)
);


select * from Cuti


CREATE TABLE Shifts (
    ID_Shift INT IDENTITY(1,1) PRIMARY KEY,
    ID_Karyawan INT NOT NULL CHECK (ID_Karyawan > 0),
    Shift_Mulai TIME NOT NULL,
    Shift_Selesai TIME NOT NULL,
    CONSTRAINT CK_Shift CHECK (Shift_Selesai > Shift_Mulai),
    Hari_Kerja NVARCHAR(20) CHECK (Hari_Kerja IN ('Senin', 'Selasa', 'Rabu', 'Kamis', 'Jumat', 'Sabtu', 'Minggu')) NOT NULL,
    FOREIGN KEY (ID_Karyawan) REFERENCES Karyawan(ID_Karyawan) ON DELETE CASCADE
);
select * from Shifts


CREATE TABLE Gaji (
    ID_Gaji INT IDENTITY(1,1) PRIMARY KEY,
    ID_Karyawan INT NOT NULL CHECK (ID_Karyawan > 0),
    Gaji_Pokok DECIMAL(18,2) CHECK (Gaji_Pokok >= 0) NOT NULL,
    Tunjangan DECIMAL(18,2) CHECK (Tunjangan >= 0) NULL,
    Potongan DECIMAL(18,2) CHECK (Potongan >= 0) NULL,
    Total_Gaji AS (Gaji_Pokok + ISNULL(Tunjangan, 0) - ISNULL(Potongan, 0)) PERSISTED,
    FOREIGN KEY (ID_Karyawan) REFERENCES Karyawan(ID_Karyawan) ON DELETE CASCADE
);
select * from Gaji

-- 1. Stored Procedure untuk menghitung jumlah absensi karyawan berdasarkan status
CREATE PROCEDURE HitungAbsensiKaryawan
    @ID_Karyawan INT
AS
BEGIN
    SELECT 
        Status, 
        COUNT(*) AS Jumlah_Hari
    FROM Kehadiran
    WHERE ID_Karyawan = @ID_Karyawan
    GROUP BY Status;
END

-- 2. View untuk laporan absensi karyawan
CREATE VIEW LaporanKehadiranKaryawan AS
SELECT 
    k.ID_Karyawan, 
    k.Nama, 
    h.Tanggal, 
    h.Status, 
    h.Waktu_Masuk, 
    h.Waktu_Keluar
FROM Karyawan k
JOIN Kehadiran h ON k.ID_Karyawan = h.ID_Karyawan;


-- 3. Trigger untuk otomatis memperbarui status cuti jika kehadiran terjadi dalam rentang cuti

CREATE TRIGGER CekStatusCuti
ON Kehadiran
AFTER INSERT
AS
BEGIN
    UPDATE Cuti
    SET Tanggal_Selesai = Tanggal_Selesai -- dummy update untuk menandai
    FROM Cuti c
    JOIN inserted i ON c.ID_Karyawan = i.ID_Karyawan
    WHERE i.Tanggal BETWEEN c.Tanggal_Mulai AND c.Tanggal_Selesai;
    -- Catatan: dalam sistem nyata, bisa ditambah kolom Status dan diubah di sini
END


-- 4. Stored Procedure untuk menampilkan jumlah absensi tiap karyawan

CREATE PROCEDURE JumlahAbsensiKaryawan
AS
BEGIN
    SELECT k.ID_Karyawan, k.Nama, COUNT(h.ID_Kehadiran) AS Jumlah_Absensi
    FROM Karyawan k
    LEFT JOIN Kehadiran h ON k.ID_Karyawan = h.ID_Karyawan
    GROUP BY k.ID_Karyawan, k.Nama;
END

EXEC JumlahAbsensiKaryawan;


--- STORED PROCEDURE UNTUK KARYAWAN
CREATE PROCEDURE TambahKaryawan -- Tambah karyawan
    @Nama VARCHAR(100),
    @Jabatan VARCHAR(50),
    @Departemen VARCHAR(50),
    @Tanggal_Masuk DATE,
    @Passwd VARCHAR(255),
    @Role VARCHAR(20)
AS
BEGIN
    INSERT INTO Karyawan (Nama, Jabatan, Departemen, Tanggal_Masuk, Passwd, Role)
    VALUES (@Nama, @Jabatan, @Departemen, @Tanggal_Masuk, @Passwd, @Role);
END;
---- ----------------------------------------------------
CREATE PROCEDURE UpdateKaryawan --UPDATE KARYAWAN
    @ID_Karyawan INT,
    @Nama VARCHAR(100),
    @Jabatan VARCHAR(50),
    @Departemen VARCHAR(50),
    @Tanggal_Masuk DATE
AS
BEGIN
    UPDATE Karyawan
    SET Nama = @Nama,
        Jabatan = @Jabatan,
        Departemen = @Departemen,
        Tanggal_Masuk = @Tanggal_Masuk
    WHERE ID_Karyawan = @ID_Karyawan;
END;

---------------------------------------------------------------------

CREATE PROCEDURE HapusKaryawan -- HAPUS KARYAWAN
    @ID_Karyawan INT
AS
BEGIN
    DELETE FROM Karyawan
    WHERE ID_Karyawan = @ID_Karyawan;
END;


DROP PROCEDURE Tambah;

-------------------------------------------
CREATE PROCEDURE TambahAttendance ---- TAMBAH ATTENDANCE KARYAWAN
    @ID_Karyawan INT,
    @Tanggal DATE,
    @Status NVARCHAR(50),
    @Waktu_Masuk DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    -- Optional: Check if the user exists (you can skip this if you want)
    IF NOT EXISTS (SELECT 1 FROM Karyawan WHERE ID_Karyawan = @ID_Karyawan)
    BEGIN
        RAISERROR('User not found in Karyawan table.', 16, 1);
        RETURN;
    END

    -- Insert attendance record
    INSERT INTO Kehadiran (ID_Karyawan, Tanggal, Status, Waktu_Masuk)
    VALUES (@ID_Karyawan, @Tanggal, @Status, @Waktu_Masuk);
END;
----------------------------------------------------------------------------------
----indexes


-- Indexes for Karyawan
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'idx_Karyawan_Nama' AND object_id = OBJECT_ID('dbo.Karyawan')
)
BEGIN
    CREATE NONCLUSTERED INDEX idx_Karyawan_Nama ON dbo.Karyawan(Nama);
    PRINT 'Created idx_Karyawan_Nama';
END

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'idx_Karyawan_Departemen' AND object_id = OBJECT_ID('dbo.Karyawan')
)
BEGIN
    CREATE NONCLUSTERED INDEX idx_Karyawan_Departemen ON dbo.Karyawan(Departemen);
    PRINT 'Created idx_Karyawan_Departemen';
END

-- Indexes for Kehadiran
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'idx_Kehadiran_IDKaryawan_Tanggal' AND object_id = OBJECT_ID('dbo.Kehadiran')
)
BEGIN
    CREATE NONCLUSTERED INDEX idx_Kehadiran_IDKaryawan_Tanggal
    ON dbo.Kehadiran(ID_Karyawan, Tanggal);
    PRINT 'Created idx_Kehadiran_IDKaryawan_Tanggal';
END

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'idx_Kehadiran_Status' AND object_id = OBJECT_ID('dbo.Kehadiran')
)
BEGIN
    CREATE NONCLUSTERED INDEX idx_Kehadiran_Status ON dbo.Kehadiran(Status);
    PRINT 'Created idx_Kehadiran_Status';
END

-- Index for Cuti (for faster lookup in trigger/join)
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'idx_Cuti_IDKaryawan_Tanggal' AND object_id = OBJECT_ID('dbo.Cuti')
)
BEGIN
    CREATE NONCLUSTERED INDEX idx_Cuti_IDKaryawan_Tanggal
    ON dbo.Cuti(ID_Karyawan, Tanggal_Mulai, Tanggal_Selesai);
    PRINT 'Created idx_Cuti_IDKaryawan_Tanggal';
END

-- Index for Shifts (lookup by karyawan and day)
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'idx_Shifts_IDKaryawan_HariKerja' AND object_id = OBJECT_ID('dbo.Shifts')
)
BEGIN
    CREATE NONCLUSTERED INDEX idx_Shifts_IDKaryawan_HariKerja
    ON dbo.Shifts(ID_Karyawan, Hari_Kerja);
    PRINT 'Created idx_Shifts_IDKaryawan_HariKerja';
END

-- Index for Gaji (frequent lookups by ID_Karyawan)
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'idx_Gaji_IDKaryawan' AND object_id = OBJECT_ID('dbo.Gaji')
)
BEGIN
    CREATE NONCLUSTERED INDEX idx_Gaji_IDKaryawan ON dbo.Gaji(ID_Karyawan);
    PRINT 'Created idx_Gaji_IDKaryawan';
END

-- Indeks untuk query optimization
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Cuti_Status_Tanggal' AND object_id = OBJECT_ID('dbo.Cuti'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Cuti_Status_Tanggal ON dbo.Cuti(Status_Persetujuan, Tanggal_Pengajuan DESC);
    PRINT 'Created idx_Cuti_Status_Tanggal';
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Cuti_Karyawan_Periode' AND object_id = OBJECT_ID('dbo.Cuti'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Cuti_Karyawan_Periode ON dbo.Cuti(ID_Karyawan, Tanggal_Mulai, Tanggal_Selesai);
    PRINT 'Created idx_Cuti_Karyawan_Periode';
END

-- Indeks tambahan untuk performa laporan dan filter
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Cuti_TanggalMulai_Status' AND object_id = OBJECT_ID('dbo.Cuti'))
BEGIN
    CREATE NONCLUSTERED INDEX idx_Cuti_TanggalMulai_Status ON dbo.Cuti(Tanggal_Mulai, Status_Persetujuan);
    PRINT 'Created idx_Cuti_TanggalMulai_Status';
END





CREATE PROCEDURE AjukanCuti
    @ID_Karyawan INT,
    @Jenis_Cuti VARCHAR(50),
    @Tanggal_Mulai DATE,
    @Tanggal_Selesai DATE,
    @Keterangan_Cuti VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON; -- Rollback otomatis jika ada error

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Validasi: Pastikan Tanggal_Mulai setidaknya 5 hari setelah tanggal pengajuan
        IF DATEDIFF(day, GETDATE(), @Tanggal_Mulai) < 5
        BEGIN
            RAISERROR('Tanggal mulai cuti harus setidaknya 5 hari setelah tanggal pengajuan.', 16, 1);
        END

        -- Validasi: Tanggal_Mulai tidak boleh lebih besar dari Tanggal_Selesai
        IF @Tanggal_Mulai > @Tanggal_Selesai
        BEGIN
            RAISERROR('Tanggal mulai cuti tidak boleh lebih dari tanggal selesai cuti.', 16, 1);
        END

        -- Validasi: Cek tumpang tindih cuti untuk karyawan yang sama
        IF EXISTS (
            SELECT 1 FROM Cuti
            WHERE ID_Karyawan = @ID_Karyawan
            AND (
                (@Tanggal_Mulai BETWEEN Tanggal_Mulai AND Tanggal_Selesai) OR
                (@Tanggal_Selesai BETWEEN Tanggal_Mulai AND Tanggal_Selesai) OR
                (Tanggal_Mulai BETWEEN @Tanggal_Mulai AND @Tanggal_Selesai)
            )
            AND Status_Persetujuan IN ('Menunggu', 'Disetujui') -- Cek hanya cuti yang aktif
        )
        BEGIN
            RAISERROR('Anda sudah memiliki pengajuan cuti yang tumpang tindih pada periode tersebut.', 16, 1);
        END

        INSERT INTO Cuti (ID_Karyawan, Jenis_Cuti, Tanggal_Mulai, Tanggal_Selesai, Keterangan_Cuti , Status_Persetujuan, Tanggal_Pengajuan)
        VALUES (@ID_Karyawan, @Jenis_Cuti, @Tanggal_Mulai, @Tanggal_Selesai, @Keterangan_Cuti , 'Menunggu', GETDATE());

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;




CREATE PROCEDURE BatalkanCuti
    @ID_Cuti INT,
    @ID_Karyawan INT -- ID Karyawan yang mengajukan cuti
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Validasi: Pastikan cuti ada dan milik karyawan yang bersangkutan
        IF NOT EXISTS (SELECT 1 FROM Cuti WHERE ID_Cuti = @ID_Cuti AND ID_Karyawan = @ID_Karyawan)
        BEGIN
            RAISERROR('Pengajuan cuti tidak ditemukan atau Anda tidak memiliki izin untuk membatalkannya.', 16, 1);
        END

        -- Validasi: Hanya cuti dengan status 'Menunggu' yang bisa dibatalkan oleh karyawan
        IF EXISTS (SELECT 1 FROM Cuti WHERE ID_Cuti = @ID_Cuti AND Status_Persetujuan <> 'Menunggu')
        BEGIN
            RAISERROR('Pengajuan cuti ini sudah diproses dan tidak dapat dibatalkan.', 16, 1);
        END

        -- Ubah status menjadi 'Dibatalkan_Karyawan'
        UPDATE Cuti
        SET
            Status_Persetujuan = 'Dibatalkan_Karyawan',
            Keterangan_Approval = 'Dibatalkan oleh karyawan yang mengajukan.',
            Tanggal_Approval = GETDATE(),
            ID_Approver = @ID_Karyawan -- ID karyawan yang membatalkan dirinya sendiri
        WHERE ID_Cuti = @ID_Cuti;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;


-- Stored Procedure: UpdateStatusCuti
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UpdateStatusCuti')
DROP PROCEDURE UpdateStatusCuti;


CREATE PROCEDURE UpdateStatusCuti
    @ID_Cuti INT,
    @NewStatus VARCHAR(20),
    @Keterangan_Approval VARCHAR(MAX) = NULL,
    @ID_Approver INT -- ID admin/HRD yang melakukan approval
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Validasi: Pastikan ID_Cuti ada
        IF NOT EXISTS (SELECT 1 FROM Cuti WHERE ID_Cuti = @ID_Cuti)
        BEGIN
            RAISERROR('Pengajuan cuti tidak ditemukan.', 16, 1);
        END

        -- Validasi: Status baru harus valid
        IF @NewStatus NOT IN ('Menunggu', 'Disetujui', 'Ditolak')
        BEGIN
            RAISERROR('Status persetujuan tidak valid. Pilih antara "Menunggu", "Disetujui", atau "Ditolak".', 16, 1);
        END

        UPDATE Cuti
        SET
            Status_Persetujuan = @NewStatus,
            Keterangan_Approval = @Keterangan_Approval,
            Tanggal_Approval = GETDATE(),
            ID_Approver = @ID_Approver
        WHERE ID_Cuti = @ID_Cuti;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;

