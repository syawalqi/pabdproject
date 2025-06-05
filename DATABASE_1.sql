CREATE DATABASE MANDAK;

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
    Tanggal_Mulai DATE NOT NULL,
    Tanggal_Selesai DATE NOT NULL,
    CONSTRAINT CK_Tanggal_Cuti CHECK (Tanggal_Selesai >= Tanggal_Mulai),
    FOREIGN KEY (ID_Karyawan) REFERENCES Karyawan(ID_Karyawan) ON DELETE CASCADE
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
