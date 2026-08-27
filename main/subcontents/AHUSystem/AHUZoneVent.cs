using main.contents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace main.subcontents.AHUSystem
{
    public partial class AHUZoneVent : Form
    {
        // 이 설비 번호 — Save_button_Click에서 저장 시 사용
        private string Num;

        // 미연결 존 헤더/박스 배경색 — HRVDiagramPanel.ZoneBgUnconnected와 동일 색 유지
        private static readonly Color UnconnectedColor = Color.FromArgb(235, 228, 248);

        public AHUZoneVent(string AHUOptions, string Num)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.Num = Num;
            Load_ZoneList(Num);
            Load_EquipmentRating(AHUOptions, Num);
            RefreshZoneTotalsRow();
            Load_FlowGrid();
            RefreshDiagram();
            RefreshAddCombo();
            RefreshRemoveCombo();
            tabControl1_SelectedIndexChanged(null, EventArgs.Empty); // 기본 선택 탭(존별 급·배기량) 기준으로 존 추가/제거 컨트롤 초기 표시 상태 맞춤
        }

        // DisplayMember 없이 객체를 담아 ToString()으로 표시 — SelectedItem에서 바로 꺼내 씀
        private class ZoneOption
        {
            public string ZoneNum;
            public string ZoneName;
            public double RequiredVent; // 이용일환기량 — "+"로 추가할 때 HRVZoneFlow에 그대로 전달
            public override string ToString() => ZoneNum + " " + ZoneName;
        }

        // 아직 Diagram_panel.Zones에 없는 존만 후보로 채움 — 자기자신/중복 추가는 이 필터링으로 차단
        private void RefreshAddCombo()
        {
            AddZone_comboBox.Items.Clear();
            string[][] AllZones = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,이용일환기량", "");
            foreach (string[] z in AllZones)
            {
                if (!Diagram_panel.Zones.Exists(existing => existing.ZoneNum == z[0]))
                {
                    double.TryParse(z[2], out double requiredVent);
                    AddZone_comboBox.Items.Add(new ZoneOption { ZoneNum = z[0], ZoneName = z[1], RequiredVent = requiredVent });
                }
            }
            AddZone_comboBox.SelectedIndex = AddZone_comboBox.Items.Count > 0 ? 0 : -1;
        }

        // SA=0/RA=0 미연결 존으로 추가 — Load_FlowGrid()가 Diagram_panel.Zones 전체를 다시
        // 훑으므로 새 존이 자동으로 헤더/후보 양쪽에 반영됨. RemoveZone과 동일하게, 재구성 전후로
        // 저장해서 다른 존의 미저장 입력을 보존.
        private void AddZone_button_Click(object sender, EventArgs e)
        {
            if (!(AddZone_comboBox.SelectedItem is ZoneOption selected))
            {
                return;
            }

            SaveVentData(); // 추가 전 현재 화면(다른 존의 미저장 입력 포함) 저장

            Diagram_panel.Zones.Add(new HRVZoneFlow
            {
                ZoneNum = selected.ZoneNum,
                ZoneName = selected.ZoneName,
                SA = 0,
                RA = 0,
                IsEquipmentConnected = false,
                RequiredVent = selected.RequiredVent
            });

            UpdateDiagramHeight();
            RefreshZoneTotalsRow();
            Load_FlowGrid();
            RefreshDiagram();
            RefreshAddCombo();
            RefreshRemoveCombo();

            SaveVentData(); // 추가 후 상태를 다시 저장 — 새 존이 DB에도 즉시 반영됨
        }

        // 설비 미연결 존만 후보로 채움 — 설비 연결 존은 임의로 뗄 대상이 아님
        private void RefreshRemoveCombo()
        {
            RemoveZone_comboBox.Items.Clear();
            foreach (HRVZoneFlow z in Diagram_panel.Zones)
            {
                if (!z.IsEquipmentConnected)
                {
                    RemoveZone_comboBox.Items.Add(new ZoneOption { ZoneNum = z.ZoneNum, ZoneName = z.ZoneName });
                }
            }
            RemoveZone_comboBox.SelectedIndex = RemoveZone_comboBox.Items.Count > 0 ? 0 : -1;
        }

        // Diagram_panel.Zones에서 제거 → Load_FlowGrid() 재구성으로 관련 헤더/행/화살표가 자동 정리됨.
        // 재구성 전후로 저장해서 다른 존의 미저장 입력을 보존.
        private void RemoveZone_button_Click(object sender, EventArgs e)
        {
            if (!(RemoveZone_comboBox.SelectedItem is ZoneOption selected))
            {
                return;
            }

            DialogResult res = MessageBox.Show(
                "선택한 존과 관련된 인접존 환기량이 함께 삭제됩니다. 계속하시겠습니까?",
                "확인", MessageBoxButtons.YesNo);
            if (res == DialogResult.No)
            {
                return;
            }

            SaveVentData(); // 제거 전 현재 화면(다른 존의 미저장 입력 포함) 저장

            Diagram_panel.Zones.RemoveAll(z => z.ZoneNum == selected.ZoneNum);

            UpdateDiagramHeight();
            RefreshZoneTotalsRow();
            Load_FlowGrid();
            RefreshDiagram();
            RefreshAddCombo();
            RefreshRemoveCombo();

            SaveVentData(); // 제거 후 상태를 다시 저장 — 이 존 관련 행이 DB에서 정리됨
        }

        // 설비 자체의 정격 급배기량(다른 화면에서 입력된 고정값) — Tab1 그리드 입력값과는 별개
        private void Load_EquipmentRating(string AHUOptions, string Num)
        {
            Diagram_panel.EquipmentTypeName = string.IsNullOrEmpty(AHUOptions) ? "설비" : AHUOptions;

            double ratedSA = 0, ratedRA = 0;
            if (AHUOptions == "열회수기")
            {
                // 열회수기는 급배기가 같은 팬을 공유하는 균형형이라 팬풍량 하나를 급/배기 양쪽에 사용
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "팬풍량", "번호 = '" + Num + "'");
                if (Value.Length > 0)
                {
                    ratedSA = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                    ratedRA = ratedSA;
                }
            }
            else if (AHUOptions == "공조기")
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "급기풍량,배기풍량", "번호 = '" + Num + "'");
                if (Value.Length > 0)
                {
                    ratedSA = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                    ratedRA = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                }
            }
            else
            {
                // 배기환기(3종): 배기 전용
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Fan", "풍량", "번호 = '" + Num + "'");
                if (Value.Length > 0)
                {
                    ratedRA = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                }
            }

            Diagram_panel.RatedSA = ratedSA;
            Diagram_panel.RatedRA = ratedRA;
        }

        private void Load_ZoneList(string Num)
        {
            // 합계 행(zoneTotalsRowIndex)만 파란색 고정 행처럼 보이도록 강제 — 나머지는 기존 자동 로직(빈칸=크림색/입력됨=흰색) 유지
            new StackedHeaderDecorator(Zone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, (cell, column, row) =>
            {
                if (row == zoneTotalsRowIndex)
                {
                    cell.Style.BackColor = Color.AliceBlue; // 위 GeneralPanel과 같은 톤(진한 파랑 대신 옅은 하늘색)
                    cell.Style.SelectionBackColor = Color.AliceBlue;
                    return true;
                }
                return false;
            });
            Zone_dataGridView.Columns.Clear();
            Zone_dataGridView.Columns.Add("A1", "존번호");
            Zone_dataGridView.Columns.Add("A2", "존이름");
            Zone_dataGridView.Columns.Add("A3", "필요환기량");
            Zone_dataGridView.Columns.Add("A4", "급기량");
            Zone_dataGridView.Columns.Add("A5", "배기량");
            // 헤더 클릭 정렬 금지 — 합계 행(zoneTotalsRowIndex) 위치가 고정 인덱스를 전제함
            foreach (DataGridViewColumn col in Zone_dataGridView.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            Zone_dataGridView.Columns["A1"].ReadOnly = true;
            Zone_dataGridView.Columns["A2"].ReadOnly = true;
            Zone_dataGridView.Columns["A3"].ReadOnly = true;

            Diagram_panel.HRVName = Num;
            Diagram_panel.Zones.Clear();

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,이용일환기량", "선택열회수기 = '" + Num + "' and 환기유무='True'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = Zone_dataGridView.Rows.Add();
                    Zone_dataGridView.Rows[nRow].Cells[0].Value = Value[n][0];
                    Zone_dataGridView.Rows[nRow].Cells[1].Value = Value[n][1];
                    Zone_dataGridView.Rows[nRow].Cells[2].Value = Value[n][2];
                    Program.UTIL.dataGridView_doubleComa(Zone_dataGridView, nRow, 2, 0);

                    // 저장된 급기량/배기량이 있으면 불러옴 (AHUZoneVent_Form은 존당 여러 행에 걸쳐 중복 저장되므로 하나만 읽으면 됨)
                    double savedSA = 0, savedRA = 0;
                    string[][] Saved = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "급기량,배기량", "설비 = '" + Num + "' And 존 = '" + Value[n][0] + "'");
                    if (Saved.Length > 0)
                    {
                        savedSA = Program.UTIL.ToDoubleOrZero(Saved[0][0]);
                        savedRA = Program.UTIL.ToDoubleOrZero(Saved[0][1]);
                        Zone_dataGridView.Rows[nRow].Cells[3].Value = savedSA.ToString("#,##0");
                        Zone_dataGridView.Rows[nRow].Cells[4].Value = savedRA.ToString("#,##0");
                    }

                    double.TryParse(Value[n][2], out double requiredVent);
                    Diagram_panel.Zones.Add(new HRVZoneFlow
                    {
                        ZoneNum = Value[n][0],
                        ZoneName = Value[n][1],
                        SA = savedSA,
                        RA = savedRA,
                        IsEquipmentConnected = true,
                        RequiredVent = requiredVent
                    });
                }
            }

            // "+"로 추가한 존은 ZoneGeneral_Form 조건에 안 걸려 재로드 시 사라지므로, AHUZoneVent_Form에
            // 저장된 존 중 누락된 것만 복원(Tab1에는 추가 안 함)
            string[][] SavedZoneNums = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "존", "설비 = '" + Num + "'");
            foreach (string[] savedZoneRow in SavedZoneNums)
            {
                string savedZoneNum = savedZoneRow[0];
                if (Diagram_panel.Zones.Exists(z => z.ZoneNum == savedZoneNum))
                {
                    continue;
                }

                string[][] SavedZoneName = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,이용일환기량", "존번호 = '" + savedZoneNum + "'");
                double.TryParse(SavedZoneName.Length > 0 ? SavedZoneName[0][1] : "", out double savedRequiredVent);

                // 미연결 존은 SA/RA 없음 — DB 과거값 대신 항상 0으로 강제(합계 오염 방지)
                Diagram_panel.Zones.Add(new HRVZoneFlow
                {
                    ZoneNum = savedZoneNum,
                    ZoneName = SavedZoneName.Length > 0 ? SavedZoneName[0][0] : "",
                    SA = 0,
                    RA = 0,
                    IsEquipmentConnected = false,
                    RequiredVent = savedRequiredVent
                });
            }

            zoneTotalsRowIndex = Zone_dataGridView.Rows.Add();
            Zone_dataGridView.Rows[zoneTotalsRowIndex].ReadOnly = true;
            Zone_dataGridView.Rows[zoneTotalsRowIndex].Cells[0].Value = "합계";
            Zone_dataGridView.Rows[zoneTotalsRowIndex].DefaultCellStyle.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);

            UpdateDiagramHeight();
            Diagram_panel.Invalidate();
        }

        // 존 박스는 고정 크기라 존이 많으면 뷰포트를 넘길 수 있음 — Diagram_panel을 키우고
        // DiagramScroll_panel(AutoScroll)이 스크롤 처리
        private void UpdateDiagramHeight()
        {
            int n = Diagram_panel.Zones.Count;
            int requiredHeight = n * 68 + Math.Max(0, n - 1) * 12 + 60; // 존 박스 높이(HRVDiagramPanel.zoneH와 동일)+간격 합 + 위아래 여백
            int viewportHeight = DiagramScroll_panel.ClientSize.Height;
            Diagram_panel.Height = Math.Max(viewportHeight, requiredHeight);
        }

        // 존별 급기량/배기량 합계 행 — 계통도의 HRV↔분기점 구간에도 이 합계를 그대로 표시(Diagram_panel.InputTotalSA/RA)
        private int zoneTotalsRowIndex = -1;

        private void RefreshZoneTotalsRow()
        {
            if (zoneTotalsRowIndex < 0)
            {
                return;
            }

            double sumSA = 0, sumRA = 0;
            foreach (HRVZoneFlow z in Diagram_panel.Zones)
            {
                // 미연결 존은 SA/RA=0이라 합계엔 영향 없지만 의미상 방어적으로 제외
                if (!z.IsEquipmentConnected)
                {
                    continue;
                }
                sumSA += z.SA;
                sumRA += z.RA;
            }

            Zone_dataGridView.Rows[zoneTotalsRowIndex].Cells[3].Value = sumSA.ToString("#,##0");
            Zone_dataGridView.Rows[zoneTotalsRowIndex].Cells[4].Value = sumRA.ToString("#,##0");

            // 정격풍량 초과 시 빨간 글씨로 경고(저장은 막지 않음) — RatedSA/RA=0(미입력)이면 비교 스킵
            bool saOverCapacity = Diagram_panel.RatedSA > 0 && sumSA > Diagram_panel.RatedSA;
            bool raOverCapacity = Diagram_panel.RatedRA > 0 && sumRA > Diagram_panel.RatedRA;
            Zone_dataGridView.Rows[zoneTotalsRowIndex].Cells[3].Style.ForeColor = saOverCapacity ? Color.Red : Color.Black;
            Zone_dataGridView.Rows[zoneTotalsRowIndex].Cells[4].Style.ForeColor = raOverCapacity ? Color.Red : Color.Black;

            Diagram_panel.InputTotalSA = sumSA;
            Diagram_panel.InputTotalRA = sumRA;
            Diagram_panel.Invalidate();
        }

        private void Zone_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                if (Zone_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    double value = Program.UTIL.dataGridView_doubleComa(Zone_dataGridView, e.RowIndex, e.ColumnIndex, 0);

                    // 행 인덱스 대신 존번호(컬럼0)로 찾음 — 나중에 정렬이 생겨도 안전하게
                    string zoneNum = Zone_dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    HRVZoneFlow editedZone = Diagram_panel.Zones.Find(z => z.ZoneNum == zoneNum);
                    if (editedZone != null)
                    {
                        if (e.ColumnIndex == 3)
                        {
                            editedZone.SA = value;
                            Diagram_panel.PulseZoneFlow(zoneNum, true);
                        }
                        else
                        {
                            editedZone.RA = value;
                            Diagram_panel.PulseZoneFlow(zoneNum, false);
                        }
                        RefreshZoneTotalsRow();

                        AutoFillRows(editedZone.ZoneNum, editedZone.RA - editedZone.SA);
                        RefreshHeaders();
                        RefreshDiagram();
                    }
                }
            }
        }

        // 이 존이 다른 헤더의 인접존(받는 쪽)으로 등장하는 미보호 행들을 "필요없음→0/필요함→빈칸"으로 재조정.
        // 미연결 존은 받는 쪽이든 보내는 쪽이든 needed와 무관하게 항상 빈칸 유지 — 직접 판단해서 입력하라는 신호이기 때문.
        private void AutoFillRows(string zoneNum, double needed)
        {
            HRVZoneFlow zone = Diagram_panel.Zones.Find(z => z.ZoneNum == zoneNum);
            bool receiverBlank = zone == null || !zone.IsEquipmentConnected;

            foreach (KeyValuePair<int, string> entry in rowReceiver)
            {
                if (entry.Value == zoneNum && !protectedRows.Contains(entry.Key))
                {
                    HRVZoneFlow senderZone = Diagram_panel.Zones.Find(z => z.ZoneNum == rowSender[entry.Key]);
                    bool senderBlank = senderZone == null || !senderZone.IsEquipmentConnected;
                    AdjacentZone_dataGridView.Rows[entry.Key].Cells[2].Value = (!receiverBlank && !senderBlank && needed <= 0) ? "0" : null;
                }
            }
        }

        // 헤더 행 인덱스 → 그 행이 나타내는 존(보내는 존), 새로고침(RefreshHeaders)에서 재사용
        private readonly Dictionary<int, HRVZoneFlow> headerZones = new Dictionary<int, HRVZoneFlow>();
        // 하위 행: 헤더=보내는 존, 인접존=받는 존 (존 → 인접존으로 환기량 이동)
        private readonly Dictionary<int, string> rowSender = new Dictionary<int, string>();
        private readonly Dictionary<int, string> rowReceiver = new Dictionary<int, string>();
        // DB에 저장된 0 아닌 값이거나 사용자가 직접 입력한 행 — 자동갱신이 건드리지 않음
        private readonly HashSet<int> protectedRows = new HashSet<int>();
        // Load_FlowGrid가 여러 번 불려도 데코레이터는 최초 1회만 등록
        private bool decoratorReady = false;

        // 존 헤더 행(보내는 존의 급배기량) + 그 아래 인접존(받는 존) 하위 행(인접존 환기량 입력)으로 구성
        private void Load_FlowGrid()
        {
            // renderColors()가 매 리페인트마다 빈칸=크림/입력값=흰색으로 덮어쓰므로, 존/인접존 컬럼(0,1)만 흰색 고정
            if (!decoratorReady)
            {
                new StackedHeaderDecorator(AdjacentZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, (cell, column, row) =>
                {
                    if (AdjacentZone_dataGridView.Rows[row].ReadOnly)
                    {
                        // 설비 미연결 존(복도 등) 헤더는 연한 보라색으로 구분, 나머지는 기존 색
                        headerZones.TryGetValue(row, out HRVZoneFlow headerZone);
                        Color headerBg = (headerZone != null && !headerZone.IsEquipmentConnected)
                            ? UnconnectedColor
                            : SystemColors.GradientActiveCaption;
                        cell.Style.BackColor = headerBg;
                        cell.Style.SelectionBackColor = headerBg;
                        return true;
                    }
                    if (column == 0 || column == 1)
                    {
                        cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                        cell.Style.ForeColor = Color.Black;
                        cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                        cell.Style.SelectionForeColor = Color.Black;
                        return true;
                    }
                    return false;
                });
                decoratorReady = true;
            }
            AdjacentZone_dataGridView.Columns.Clear();
            AdjacentZone_dataGridView.Columns.Add("B1", "존(기계환기량)");
            AdjacentZone_dataGridView.Columns.Add("B2", "인접존");
            AdjacentZone_dataGridView.Columns.Add("B3", "인접존 환기량");
            // 헤더 클릭 정렬 금지 — 딕셔너리들이 전부 행 인덱스로 매핑돼 있어 정렬되면 깨짐
            foreach (DataGridViewColumn col in AdjacentZone_dataGridView.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            AdjacentZone_dataGridView.Rows.Clear();
            headerZones.Clear();
            rowSender.Clear();
            rowReceiver.Clear();
            protectedRows.Clear();

            foreach (HRVZoneFlow zone in Diagram_panel.Zones)
            {
                int headerRow = AdjacentZone_dataGridView.Rows.Add();
                headerZones[headerRow] = zone;
                // 미연결 존 헤더는 배경색으로 구분(실제 색은 RenderProc이 재적용)
                AdjacentZone_dataGridView.Rows[headerRow].DefaultCellStyle.BackColor =
                    zone.IsEquipmentConnected ? SystemColors.GradientActiveCaption : UnconnectedColor;
                AdjacentZone_dataGridView.Rows[headerRow].DefaultCellStyle.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
                AdjacentZone_dataGridView.Rows[headerRow].ReadOnly = true;

                // 인접존 후보는 "물리적으로 벽/슬래브가 맞닿은 존"이면서 동시에 이 다이어그램에 있는 존
                // (Diagram_panel.Zones)만 — 물리적 인접만으로 걸면 "+"로 추가 안 한 존까지 후보로 뜨므로 둘 다 검사
                List<string> adjacentNums = new List<string>();
                List<string> adjacentNames = new List<string>();

                HashSet<string> physicallyAdjacent = new HashSet<string>();
                string[][] AdjWalls = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "인접존", "존 = '" + zone.ZoneNum + "' And 외피유형 = '내벽'");
                foreach (string[] row in AdjWalls) { if (!string.IsNullOrEmpty(row[0])) { physicallyAdjacent.Add(row[0]); } }
                string[][] AdjSlabs = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "인접존", "존 = '" + zone.ZoneNum + "' And 외피유형 = '층간바닥'");
                foreach (string[] row in AdjSlabs) { if (!string.IsNullOrEmpty(row[0])) { physicallyAdjacent.Add(row[0]); } }

                foreach (HRVZoneFlow other in Diagram_panel.Zones)
                {
                    if (other.ZoneNum != zone.ZoneNum && physicallyAdjacent.Contains(other.ZoneNum) && !adjacentNums.Contains(other.ZoneNum))
                    {
                        adjacentNums.Add(other.ZoneNum);
                        adjacentNames.Add(other.ZoneName);
                    }
                }

                for (int a = 0; a < adjacentNums.Count; a++)
                {
                    int subRow = AdjacentZone_dataGridView.Rows.Add();
                    rowSender[subRow] = zone.ZoneNum;
                    rowReceiver[subRow] = adjacentNums[a];

                    AdjacentZone_dataGridView.Rows[subRow].Cells[0].ReadOnly = true;
                    AdjacentZone_dataGridView.Rows[subRow].Cells[1].Value = adjacentNums[a] + " " + adjacentNames[a];
                    AdjacentZone_dataGridView.Rows[subRow].Cells[1].ReadOnly = true;

                    // 저장값 있으면 사용, 없으면 보내는 존/받는 존 둘 다 연결 상태고 받는 존이 필요 없을 때만 0 자동채움.
                    // 저장된 0은 "실제 0"인지 "자동채움"인지 구분 안 되므로 0이 아닌 값만 보호 대상.
                    string[][] SavedAdj = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "인접존배기량", "설비 = '" + Num + "' And 존 = '" + zone.ZoneNum + "' And 인접존 = '" + adjacentNums[a] + "'");
                    if (SavedAdj.Length > 0)
                    {
                        double savedValue = Program.UTIL.ToDoubleOrZero(SavedAdj[0][0]);
                        AdjacentZone_dataGridView.Rows[subRow].Cells[2].Value = savedValue.ToString("#,##0");
                        if (savedValue != 0)
                        {
                            protectedRows.Add(subRow);
                        }
                    }
                    else
                    {
                        HRVZoneFlow receiverZone = Diagram_panel.Zones.Find(z => z.ZoneNum == adjacentNums[a]);
                        double receiverNeeded = receiverZone != null ? receiverZone.RA - receiverZone.SA : 0;
                        if (zone.IsEquipmentConnected && receiverZone != null && receiverZone.IsEquipmentConnected && receiverNeeded <= 0)
                        {
                            AdjacentZone_dataGridView.Rows[subRow].Cells[2].Value = "0"; // 보내는 존/받는 존 중 하나라도 미연결이면 빈칸 유지
                        }
                    }
                }

                SetHeaderRow(headerRow, zone);
            }
        }

        // 이 존이 받는 쪽으로 등장하는 모든 행(여러 헤더에 흩어질 수 있음)의 환기량 합계
        private double SumIncoming(string receiverZoneNum)
        {
            double sum = 0;
            foreach (KeyValuePair<int, string> entry in rowReceiver)
            {
                if (entry.Value == receiverZoneNum)
                {
                    object cellValue = AdjacentZone_dataGridView.Rows[entry.Key].Cells[2].Value;
                    if (cellValue != null && double.TryParse(cellValue.ToString().Replace(",", ""), out double v))
                    {
                        sum += v;
                    }
                }
            }
            return sum;
        }

        // 헤더 급배기량 텍스트만 갱신 — 필요환기량은 받는 쪽 기준이라 다른 헤더들에 흩어져 여기 표시 안 함
        private void SetHeaderRow(int rowIndex, HRVZoneFlow zone)
        {
            string connLabel = zone.IsEquipmentConnected ? "" : " (기계환기 미설치)";
            AdjacentZone_dataGridView.Rows[rowIndex].Cells[0].Value = zone.ZoneNum + connLabel + " : 급기 " + zone.SA.ToString("0") + " CMH, 배기 " + zone.RA.ToString("0") + " CMH";
            AdjacentZone_dataGridView.Rows[rowIndex].Cells[2].Value = null;
            AdjacentZone_dataGridView.InvalidateRow(rowIndex);
        }

        // 헤더 텍스트만 갱신, 하위 행은 안 건드림
        private void RefreshHeaders()
        {
            foreach (KeyValuePair<int, HRVZoneFlow> entry in headerZones)
            {
                SetHeaderRow(entry.Key, entry.Value);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            bool onAdjacentTab = tabControl1.SelectedTab == AdjacentZone_tabPage;
            if (onAdjacentTab)
            {
                RefreshHeaders();
            }
            Diagram_panel.ShowAdjacentFlow = onAdjacentTab;
            Diagram_panel.Invalidate();

            // 존 추가/제거는 인접존 환기량(미연결 존 배분) 작업에서만 필요 — 존별 급·배기량 탭에서는 숨김
            AddZone_label.Visible = onAdjacentTab;
            AddZone_comboBox.Visible = onAdjacentTab;
            AddZone_button.Visible = onAdjacentTab;
            RemoveZone_label.Visible = onAdjacentTab;
            RemoveZone_comboBox.Visible = onAdjacentTab;
            RemoveZone_button.Visible = onAdjacentTab;
        }

        private void AdjacentZone_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                object rawValue = AdjacentZone_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (rawValue != null && rawValue.ToString().Trim() != "" &&
                    (!double.TryParse(rawValue.ToString().Replace(",", ""), out double rawNum) || rawNum < 0))
                {
                    MessageBox.Show("0 이상의 숫자를 입력하세요.");
                    AdjacentZone_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                }

                if (AdjacentZone_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    Program.UTIL.dataGridView_doubleComa(AdjacentZone_dataGridView, e.RowIndex, e.ColumnIndex, 0);
                    protectedRows.Add(e.RowIndex); // 실제 값을 입력함 — 이후 자동갱신(AutoFillRows)이 건드리지 않음
                }
                else if (protectedRows.Remove(e.RowIndex))
                {
                    // 보호 해제된 빈칸을 받는 존 자신의 필요 여부로 재평가
                    string receiverZoneNum = rowReceiver[e.RowIndex];
                    HRVZoneFlow receiverZone = Diagram_panel.Zones.Find(z => z.ZoneNum == receiverZoneNum);
                    if (receiverZone != null)
                    {
                        AutoFillRows(receiverZoneNum, receiverZone.RA - receiverZone.SA);
                    }
                }
                RefreshDiagram();
            }
        }

        // 입력값을 계통도 화살표로 반영 — 헤더(보내는 존)→인접존(받는 존) 방향
        private void RefreshDiagram()
        {
            Diagram_panel.AdjacentFlows.Clear();
            foreach (KeyValuePair<int, string> entry in rowSender)
            {
                int rowIndex = entry.Key;
                string senderZoneNum = entry.Value;
                string receiverZoneNum = rowReceiver[rowIndex];

                object cellValue = AdjacentZone_dataGridView.Rows[rowIndex].Cells[2].Value;
                if (cellValue != null && double.TryParse(cellValue.ToString().Replace(",", ""), out double amount) && amount > 0)
                {
                    Diagram_panel.AdjacentFlows.Add(new AdjacentZoneFlow
                    {
                        FromZoneNum = senderZoneNum,
                        ToZoneNum = receiverZoneNum,
                        Amount = amount
                    });
                }
            }
            Diagram_panel.Invalidate();
        }

        // 저장 전 검증 — 빈칸→0은 계속 진행 가능한 확인, 필요환기량 부족은 저장을 막는 오류(첫 번째 존만 안내).
        private bool ValidateBeforeSave()
        {
            List<int> blankRows = new List<int>();
            foreach (KeyValuePair<int, string> entry in rowSender)
            {
                int rowIndex = entry.Key;
                object cellValue = AdjacentZone_dataGridView.Rows[rowIndex].Cells[2].Value;
                if (cellValue == null || cellValue.ToString().Trim() == "")
                {
                    blankRows.Add(rowIndex);
                }
                else if (!double.TryParse(cellValue.ToString().Replace(",", ""), out double v) || v < 0)
                {
                    MessageBox.Show("0 이상의 숫자를 입력하세요.");
                    return false;
                }
            }

            if (blankRows.Count > 0)
            {
                DialogResult res = MessageBox.Show("빈칸은 0 CMH로 저장됩니다. 계속 저장하시겠습니까?", "확인", MessageBoxButtons.YesNo);
                if (res == DialogResult.No) { return false; }
            }

            // 필요환기량은 받는 쪽 기준 — 여러 헤더에 흩어질 수 있어 전체 그리드에서 합산(SumIncoming).
            // 부족한 존을 발견하는 즉시 그 존만 안내하고 저장을 막음(여러 존을 한꺼번에 나열하지 않음).
            foreach (HRVZoneFlow zone in Diagram_panel.Zones)
            {
                double needed = zone.RA - zone.SA;
                if (needed <= 0) { continue; }

                double inputSum = SumIncoming(zone.ZoneNum);
                double shortfall = needed - inputSum;
                if (shortfall > 0.5) // 반올림 오차 허용
                {
                    MessageBox.Show(zone.ZoneNum + "의 급배기 밸런스량은 " + shortfall.ToString("0") + "CMH 입니다. 인접존 환기량을 확인하세요.");
                    return false;
                }
            }

            return true;
        }

        // 화면 상태를 AHUZoneVent_Form에 반영(검증/폼 닫기 없음) — Save/RemoveZone이 공유해서 씀.
        // 인접존 후보가 없는 존(예: 단일 존)은 인접존 빈 행 하나로 급배기량만 저장.
        private void SaveVentData()
        {
            Program.DB.deleteValue(DB.type.ProjDB, "AHUZoneVent_Form", "설비 = '" + Num + "'");

            foreach (HRVZoneFlow zone in Diagram_panel.Zones)
            {
                List<int> ownRows = new List<int>();
                foreach (KeyValuePair<int, string> entry in rowSender)
                {
                    if (entry.Value == zone.ZoneNum)
                    {
                        ownRows.Add(entry.Key);
                    }
                }

                if (ownRows.Count > 0)
                {
                    foreach (int rowIndex in ownRows)
                    {
                        string receiverZoneNum = rowReceiver[rowIndex];
                        object cellValue = AdjacentZone_dataGridView.Rows[rowIndex].Cells[2].Value;
                        double adjExhaust = 0;
                        if (cellValue != null)
                        {
                            double.TryParse(cellValue.ToString().Replace(",", ""), out adjExhaust);
                        }

                        Program.DB.setValue(DB.type.ProjDB, "AHUZoneVent_Form",
                            "설비,존,급기량,배기량,인접존,인접존배기량",
                            "'" + Num + "','" + zone.ZoneNum + "','" + zone.SA + "','" + zone.RA + "','" + receiverZoneNum + "','" + adjExhaust + "'",
                            "");
                    }
                }
                else
                {
                    Program.DB.setValue(DB.type.ProjDB, "AHUZoneVent_Form",
                        "설비,존,급기량,배기량,인접존,인접존배기량",
                        "'" + Num + "','" + zone.ZoneNum + "','" + zone.SA + "','" + zone.RA + "','','0'",
                        "");
                }
            }

            
        }

        // 실시간 저장 대신 SAVE 버튼을 눌렀을 때만 반영(다른 subcontents 폼들과 동일한 관례).
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeSave())
            {
                return;
            }

            SaveVentData();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    // 존별 급배기량 데이터
    public class HRVZoneFlow
    {
        public string ZoneNum   { get; set; }
        public string ZoneName  { get; set; }
        public double SA        { get; set; }  // 급기량 m³/h
        public double RA        { get; set; }  // 배기량 m³/h
        // 설비 연결 존이면 true, "+"로 추가/복원된 존은 false — 다이어그램 SA/RA 연결선 표시 기준
        public bool IsEquipmentConnected { get; set; }
        public double RequiredVent { get; set; } // 필요환기량(ZoneGeneral_Form.이용일환기량) — 다이어그램 안내 박스에 표시
    }

    // 존간 인접 환기 이동 데이터(인접존 환기량 탭 입력값)
    public class AdjacentZoneFlow
    {
        public string FromZoneNum { get; set; } // 보내는 존
        public string ToZoneNum   { get; set; } // 받는 존
        public double Amount      { get; set; } // m³/h
    }

    // 계통도를 그리는 Panel
    public class HRVDiagramPanel : Panel
    {
        // ── 퍼블릭 데이터 ──────────────────────────────────────────
        public string HRVName       { get; set; } = "HRV01";
        public string EquipmentTypeName { get; set; } = "열회수기"; // 열회수기/공조기/배기환기(3종) — 설비 타입에 따라 동적으로 설정됨
        public double RatedSA       { get; set; } // 이 설비 자체의 급기 정격풍량(CMH) — 설비 정보 화면에서 입력된 고정값
        public double RatedRA       { get; set; } // 이 설비 자체의 배기 정격풍량(CMH)
        public double InputTotalSA  { get; set; } // 존별 급배기량 그리드의 급기량 합계 — HRV↔분기점 구간에 표시
        public double InputTotalRA  { get; set; } // 존별 급배기량 그리드의 배기량 합계
        public List<HRVZoneFlow> Zones { get; set; } = new List<HRVZoneFlow>();
        public List<AdjacentZoneFlow> AdjacentFlows { get; set; } = new List<AdjacentZoneFlow>();
        public bool ShowAdjacentFlow { get; set; } = false; // "인접존 환기량" 탭 선택 시에만 켜짐(좌측 정렬 + 화살표)

        // ── 색상 ───────────────────────────────────────────────────
        static readonly Color SAColor   = Color.FromArgb(24, 95, 165);
        static readonly Color RAColor   = Color.FromArgb(153, 60, 29);
        static readonly Color HRVBg     = Color.FromArgb(230, 241, 251);
        static readonly Color HRVBorder = Color.FromArgb(55, 138, 221);
        static readonly Color ZoneBg    = Color.White;
        static readonly Color ZoneBgUnconnected = Color.FromArgb(235, 228, 248); // 설비 미연결 존(복도 등) — AHUZoneVent.UnconnectedColor와 동일 색
        static readonly Color ZoneBorder= Color.FromArgb(200, 200, 200);
        static readonly Color OAColor   = Color.FromArgb(59, 109, 17);
        static readonly Color EAColor   = Color.FromArgb(133, 79, 11);
        static readonly Color TextMain  = Color.FromArgb(26, 26, 26);
        static readonly Color TextMuted = Color.FromArgb(120, 120, 120);
        static readonly Color AdjacentColor = Color.FromArgb(120, 100, 200); // 인접존 환기 이동(보라색)

        // ── 입력 강조(펄스) ────────────────────────────────────────
        const float PulseSize   = 14f;
        const float NormalSize  = 8f;
        const int   PulseHoldMs = 450; // 이 시간 동안만 크게 보여주고, 지나면 바로 원래 크기로 스냅

        System.Windows.Forms.Timer pulseTimer;
        string pulseZoneNum;
        bool pulseIsSA;
        bool isPulsing;

        public HRVDiagramPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw   = true;
        }

        // 급기/배기 값이 막 입력된 존의 라벨을 잠깐 크고 굵게 보였다가 원래 크기로 바로 되돌림
        public void PulseZoneFlow(string zoneNum, bool isSA)
        {
            pulseZoneNum = zoneNum;
            pulseIsSA = isSA;
            isPulsing = true;

            if (pulseTimer == null)
            {
                pulseTimer = new System.Windows.Forms.Timer { Interval = PulseHoldMs };
                pulseTimer.Tick += (s, e) =>
                {
                    isPulsing = false;
                    pulseTimer.Stop();
                    Invalidate();
                };
            }
            pulseTimer.Stop();
            pulseTimer.Start();
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pulseTimer?.Stop();
                pulseTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode   = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (Zones == null || Zones.Count == 0) return;

            int W = ClientSize.Width;
            int H = ClientSize.Height;
            int pad = 16;
            // 인접존 탭일 때만 오른쪽 여백 확보(화살표+안내박스용) — 흐름 수만큼 필요 폭이 늘지만, HRV/존
            // 박스 최소폭(minUsableW)은 항상 보장(상한 걸리면 화살표만 겹쳐 그림)
            int infoBoxW = 130;
            int curveToBoxGap = 45; // 화살표 수치 라벨(예: "1500")이 안내 박스와 안 겹치도록 곡선-박스 사이 최소 간격
            int infoBoxReserve = ShowAdjacentFlow ? infoBoxW + curveToBoxGap : 0;
            int flowCount = ShowAdjacentFlow && AdjacentFlows != null ? AdjacentFlows.Count : 0;
            int requiredBulgeSpace = (flowCount > 0 ? 40 + (flowCount - 1) * 30 + 90 : 0) + infoBoxReserve;
            int minUsableW = 460; // HRV박스(120)+OA/EA(42)+존박스(140)+여백(SA/RA 라벨용 통로 포함)
            int maxMargin = Math.Max(0, W - minUsableW);
            int adjacentZoneMargin = ShowAdjacentFlow ? Math.Min(Math.Max(300, requiredBulgeSpace), maxMargin) : 0;
            int usableW = W - adjacentZoneMargin;
            // 화살표 곡선이 뻗을 수 있는 최대 돌출폭 — adjacentZoneMargin 중 안내박스 몫(infoBoxReserve)을 뺀 나머지
            int curveMaxBulge = ShowAdjacentFlow ? Math.Max(40, adjacentZoneMargin - infoBoxReserve) : 40;
            // 안내 박스는 화살표 최대 돌출폭 바로 뒤(curveToBoxGap)에 위치 — 창이 넓어도 화살표와 붙어있어 빈 공간이 안 생김.
            // (curveMaxBulge가 잘리지 않는 한) 항상 오른쪽 끝(W-pad)에 딱 맞게 끝남
            int infoBoxX = (usableW - pad) + curveMaxBulge + curveToBoxGap;

            // ── HRV 박스 ─────────────────────────────────────────
            int hrvW = 120, hrvH = 130; // SA/RA 정격값 자릿수가 늘어나도(예: 2000CMH) 한 줄에 안 밀리도록 넉넉하게
            int hrvX = usableW / 2 - hrvW / 2;
            int hrvY = H / 2 - hrvH / 2;

            DrawRoundRect(g, hrvX, hrvY, hrvW, hrvH, 8, HRVBg, HRVBorder);

            using var fBold = new Font("맑은 고딕", 9f, FontStyle.Bold);
            using var fNorm = new Font("맑은 고딕", 8f, FontStyle.Regular);

            // 이 메서드 안에서 여러 번 재사용되는 브러시들 — 매번 new SolidBrush(...)로 새로 만들지 않고 한 번만 만들어 재사용,
            // OnPaint가 끝날 때 using으로 자동 해제(잦은 Invalidate/애니메이션에도 GDI 리소스가 누적되지 않도록)
            using var saBrush = new SolidBrush(SAColor);
            using var raBrush = new SolidBrush(RAColor);
            using var textMainBrush = new SolidBrush(TextMain);

            DrawCenteredString(g, HRVName, fBold, TextMain,
                new Rectangle(hrvX, hrvY + 4, hrvW, 18));
            DrawCenteredString(g, EquipmentTypeName, fNorm, TextMuted,
                new Rectangle(hrvX, hrvY + 20, hrvW, 16));

            // SA / RA 레이블 배경 — 이 설비 자체의 정격풍량(RatedSA/RatedRA, 설비 정보 화면에서 입력된 고정값)
            int saLabelY = hrvY + 56;
            int raLabelY = hrvY + 84;
            using var saLabelBgBrush = new SolidBrush(Color.FromArgb(210, 235, 250));
            g.FillRectangle(saLabelBgBrush,
                hrvX + 8, saLabelY, hrvW - 16, 18);
            DrawCenteredString(g, "SA → " + RatedSA.ToString("0") + "CMH", fNorm, SAColor,
                new Rectangle(hrvX + 8, saLabelY, hrvW - 16, 18));

            using var raLabelBgBrush = new SolidBrush(Color.FromArgb(250, 235, 210));
            g.FillRectangle(raLabelBgBrush,
                hrvX + 8, raLabelY, hrvW - 16, 18);
            DrawCenteredString(g, "RA ← " + RatedRA.ToString("0") + "CMH", fNorm, RAColor,
                new Rectangle(hrvX + 8, raLabelY, hrvW - 16, 18));

            // ── OA / EA 박스 ────────────────────────────────────
            int oaBoxW = 42, oaBoxH = 24;
            int oaX = 50, oaY = saLabelY + 9 - oaBoxH / 2;
            int eaY = raLabelY + 9 - oaBoxH / 2;

            DrawRoundRect(g, oaX, oaY, oaBoxW, oaBoxH, 4,
                Color.FromArgb(232, 243, 224), OAColor);
            DrawCenteredString(g, "OA", fBold, OAColor,
                new Rectangle(oaX, oaY, oaBoxW, oaBoxH));

            DrawRoundRect(g, oaX, eaY, oaBoxW, oaBoxH, 4,
                Color.FromArgb(250, 238, 218), EAColor);
            DrawCenteredString(g, "EA", fBold, EAColor,
                new Rectangle(oaX, eaY, oaBoxW, oaBoxH));

            // OA→HRV, HRV→EA 화살표
            int oaMidY = oaY + oaBoxH / 2;
            int eaMidY = eaY + oaBoxH / 2;
            DrawArrow(g, oaX + oaBoxW, oaMidY, hrvX, oaMidY, OAColor);
            DrawArrow(g, hrvX, eaMidY, oaX + oaBoxW, eaMidY, EAColor);

            // ── 존 박스 + 연결선 ─────────────────────────────────
            // zoneW: 필요환기량 줄 텍스트가 안 잘리게 110→140. zoneH: 줄이 하나 늘어서 52→68(AHUZoneVent.UpdateDiagramHeight()의 하드코딩 값과 반드시 동기화)
            int zoneW = 140, zoneH = 68;
            int zoneX = usableW - pad - zoneW;
            int n = Zones.Count;
            int totalZoneHeight = n * zoneH + (n - 1) * 12;
            int zoneStartY = (H - totalZoneHeight) / 2;

            int saCenterY = saLabelY + 9;
            int raCenterY = raLabelY + 9;
            int midX = hrvX + hrvW + (zoneX - hrvX - hrvW) / 2;

            // HRV↔분기점(midX) 구간 — 각 존으로 갈라지기 전, 존별 급배기량 그리드의 합계를 한 번만 표시
            using (var fTrunk = new Font("맑은 고딕", 8f))
            {
                int trunkLabelX = hrvX + hrvW + 6;
                g.DrawString(InputTotalSA.ToString("0") + " m³/h", fTrunk, saBrush, trunkLabelX, saCenterY - 14);
                g.DrawString(InputTotalRA.ToString("0") + " m³/h", fTrunk, raBrush, trunkLabelX, raCenterY + 2);
            }

            for (int i = 0; i < n; i++)
            {
                var z = Zones[i];
                int zy = zoneStartY + i * (zoneH + 12);
                int zMidY = zy + zoneH / 2;

                // 미연결 존은 배경색/이름에 "(미연결)" 표시로 구분
                DrawRoundRect(g, zoneX, zy, zoneW, zoneH, 6, z.IsEquipmentConnected ? ZoneBg : ZoneBgUnconnected, ZoneBorder);
                DrawCenteredString(g, z.ZoneNum, fBold, TextMain,
                    new Rectangle(zoneX, zMidY - 24, zoneW, 18));
                DrawCenteredString(g, z.ZoneName + (z.IsEquipmentConnected ? "" : " (미연결)"), fNorm, TextMuted,
                    new Rectangle(zoneX, zMidY - 6, zoneW, 16));
                DrawCenteredString(g, "필요환기량 " + z.RequiredVent.ToString("0") + "CMH", fNorm, TextMuted,
                    new Rectangle(zoneX, zMidY + 10, zoneW, 16));

                // 밸런스 불균형/추가필요량 안내 박스 — 인접존 환기량 탭에서만, 조건에 걸리는 존만 그림(둘 다 걸리면 밸런스 우선)
                if (ShowAdjacentFlow)
                {
                    double incoming = 0;
                    if (AdjacentFlows != null)
                    {
                        foreach (AdjacentZoneFlow flow in AdjacentFlows)
                        {
                            if (flow.ToZoneNum == z.ZoneNum) { incoming += flow.Amount; }
                        }
                    }
                    double balanceGap = (z.RA - z.SA) - incoming;
                    double additionalNeed = z.RequiredVent - (z.SA + incoming);

                    string infoText = null;
                    if (balanceGap > 0.5) { infoText = "밸런스 불균형량 : " + balanceGap.ToString("0") + "CMH"; }
                    else if (additionalNeed > 0.5) { infoText = additionalNeed.ToString("0") + "CMH 추가 필요"; }

                    if (infoText != null)
                    {
                        // 안내 박스는 항상 패널 오른쪽 끝(infoBoxX)에 고정 — 화살표는 그 앞에서 멈춤(curveMaxBulge)
                        DrawRoundRect(g, infoBoxX, zy, infoBoxW, zoneH, 6, Color.FromArgb(255, 247, 214), Color.FromArgb(210, 180, 90));
                        DrawCenteredString(g, infoText, fNorm, TextMain, new Rectangle(infoBoxX + 4, zy, infoBoxW - 8, zoneH));
                    }
                }

                // 미연결 존은 SA/RA를 안 주고받으므로 HRV 연결선/라벨은 생략(0 CMH 연결로 오해 방지)
                if (z.IsEquipmentConnected)
                {
                    // SA 연결선: HRV SA출구 → midX 수직 → 존 중간
                    int saY = zMidY - 6;
                    DrawPolyline(g, SAColor,
                        new Point(hrvX + hrvW, saCenterY),
                        new Point(midX, saCenterY),
                        new Point(midX, saY),
                        new Point(zoneX, saY));
                    // 화살표 끝
                    DrawArrowHead(g, zoneX + 12, saY, SAColor, false); // SA ▶

                    // RA 연결선: 존 → midX+12 수직 → HRV RA입구
                    int raY = zMidY + 6;
                    DrawPolyline(g, RAColor,
                        new Point(zoneX, raY),
                        new Point(midX + 12, raY),
                        new Point(midX + 12, raCenterY),
                        new Point(hrvX + hrvW, raCenterY));
                    // 화살표 끝
                    DrawArrowHead(g, zoneX + 4, raY, RAColor, true);  // RA ◀

                    // 풍량 레이블 — 방금 입력된 값은 잠깐 크고 굵게 보였다가 원래 크기로 바로 스냅됨
                    bool saPulsing = isPulsing && pulseIsSA && z.ZoneNum == pulseZoneNum;
                    bool raPulsing = isPulsing && !pulseIsSA && z.ZoneNum == pulseZoneNum;

                    using var fSA = new Font("맑은 고딕", saPulsing ? PulseSize : NormalSize, saPulsing ? FontStyle.Bold : FontStyle.Regular);
                    using var fRA = new Font("맑은 고딕", raPulsing ? PulseSize : NormalSize, raPulsing ? FontStyle.Bold : FontStyle.Regular);

                    // 존 박스에 바짝 붙여(labelGap) 우측 정렬 — 자릿수/폰트 크기와 무관하게 항상 박스 바로 옆에 위치
                    string saText = $"SA {z.SA:0} m³/h";
                    string raText = $"RA {z.RA:0} m³/h";
                    float saTextW = g.MeasureString(saText, fSA).Width;
                    float raTextW = g.MeasureString(raText, fRA).Width;
                    int labelGap = 2;
                    g.DrawString(saText, fSA, saBrush, zoneX - labelGap - saTextW, saY - 14);
                    g.DrawString(raText, fRA, raBrush, zoneX - labelGap - raTextW, raY + 4);
                }
            }

            // ── 인접존 환기 이동(점선, 보라색) — "인접존 환기량" 탭 선택 시(ShowAdjacentFlow)만 그림 ──
            if (ShowAdjacentFlow && AdjacentFlows != null && AdjacentFlows.Count > 0)
            {
                using var fAdj = new Font("맑은 고딕", 8f);
                using var penAdj = new Pen(AdjacentColor, 1.6f) { DashStyle = DashStyle.Dash };
                using var adjacentBrush = new SolidBrush(AdjacentColor);

                for (int k = 0; k < AdjacentFlows.Count; k++)
                {
                    AdjacentZoneFlow flow = AdjacentFlows[k];
                    int idxFrom = Zones.FindIndex(z => z.ZoneNum == flow.FromZoneNum);
                    int idxTo = Zones.FindIndex(z => z.ZoneNum == flow.ToZoneNum);
                    if (idxFrom < 0 || idxTo < 0) continue;

                    int yFrom = zoneStartY + idxFrom * (zoneH + 12) + zoneH / 2;
                    int yTo = zoneStartY + idxTo * (zoneH + 12) + zoneH / 2;
                    // 곡선을 하나씩 더 바깥으로 밀되 curveMaxBulge 안으로 제한 — 그 바깥은 안내 박스 자리라 침범하지 않음
                    int bulge = Math.Min(40 + k * 30, curveMaxBulge);

                    Point p0 = new Point(zoneX + zoneW, yFrom);
                    Point p3 = new Point(zoneX + zoneW, yTo);
                    Point p1 = new Point(zoneX + zoneW + bulge, yFrom);
                    Point p2 = new Point(zoneX + zoneW + bulge, yTo);

                    g.DrawBezier(penAdj, p0, p1, p2, p3);
                    DrawArrowHead(g, p3.X + 8, p3.Y, AdjacentColor, true); // 받는 존 쪽으로 화살표

                    g.DrawString(flow.Amount.ToString("0"), fAdj, adjacentBrush,
                        zoneX + zoneW + bulge + 8, (yFrom + yTo) / 2 - 8);
                }
            }

            // ── 범례 ────────────────────────────────────────────
            using var fLegend = new Font("맑은 고딕", 8f);
            g.FillRectangle(saBrush, pad, H - 18, 10, 10);
            g.DrawString("급기(SA)", fLegend, textMainBrush, pad + 14, H - 18);
            g.FillRectangle(raBrush, pad + 80, H - 18, 10, 10);
            g.DrawString("배기(RA)", fLegend, textMainBrush, pad + 94, H - 18);
            if (ShowAdjacentFlow)
            {
                using (var penLegend = new Pen(AdjacentColor, 1.6f) { DashStyle = DashStyle.Dash })
                {
                    g.DrawLine(penLegend, pad + 160, H - 13, pad + 184, H - 13);
                }
                g.DrawString("인접존 환기", fLegend, textMainBrush, pad + 188, H - 18);
            }
        }

        // ── 헬퍼 메서드 ──────────────────────────────────────────

        static void DrawRoundRect(Graphics g, int x, int y, int w, int h,
            int r, Color fill, Color stroke)
        {
            using var path = RoundRectPath(x, y, w, h, r);
            using var brush = new SolidBrush(fill);
            using var pen = new Pen(stroke, 1f);
            g.FillPath(brush, path);
            g.DrawPath(pen, path);
        }

        static GraphicsPath RoundRectPath(int x, int y, int w, int h, int r)
        {
            var p = new GraphicsPath();
            p.AddArc(x, y, r * 2, r * 2, 180, 90);
            p.AddArc(x + w - r * 2, y, r * 2, r * 2, 270, 90);
            p.AddArc(x + w - r * 2, y + h - r * 2, r * 2, r * 2, 0, 90);
            p.AddArc(x, y + h - r * 2, r * 2, r * 2, 90, 90);
            p.CloseFigure();
            return p;
        }

        static void DrawCenteredString(Graphics g, string text, Font font,
            Color color, Rectangle rect)
        {
            using var sf = new StringFormat
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using var brush = new SolidBrush(color);
            g.DrawString(text, font, brush, rect, sf);
        }

        static void DrawArrow(Graphics g, int x1, int y1, int x2, int y2, Color color)
        {
            using var pen = new Pen(color, 1.5f) { EndCap = LineCap.ArrowAnchor };
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        static void DrawPolyline(Graphics g, Color color, params Point[] pts)
        {
            using var pen = new Pen(color, 1.5f);
            g.DrawLines(pen, pts);
        }

        static void DrawArrowHead(Graphics g, int x, int y, Color color, bool pointLeft)
        {
            var pts = pointLeft
                ? new[] { new Point(x, y), new Point(x + 8, y - 4), new Point(x + 8, y + 4) }
                : new[] { new Point(x, y), new Point(x - 8, y - 4), new Point(x - 8, y + 4) };
            using var brush = new SolidBrush(color);
            g.FillPolygon(brush, pts);
        }
    }
}
