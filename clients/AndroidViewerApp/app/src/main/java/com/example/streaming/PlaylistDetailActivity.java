package com.example.streaming;

import android.os.Bundle;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.streaming.models.PlaylistItem;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PlaylistDetailActivity extends AppCompatActivity {
    private PlaylistItemAdapter adapter;
    private ProgressBar progressBar;
    private TextView titleView;
    private PlaylistService service;
    private int playlistId;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_playlist_detail);

        titleView = findViewById(R.id.detailTitle);
        progressBar = findViewById(R.id.detailLoading);
        RecyclerView recyclerView = findViewById(R.id.detailList);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new PlaylistItemAdapter(new ArrayList<>());
        recyclerView.setAdapter(adapter);

        playlistId = getIntent().getIntExtra("PLAYLIST_ID", -1);
        String title = getIntent().getStringExtra("PLAYLIST_TITLE");
        titleView.setText(title);

        service = RetrofitClient.getInstance();
        loadItems();
    }

    private void loadItems() {
        progressBar.setVisibility(View.VISIBLE);
        service.getPlaylistItems(playlistId).enqueue(new Callback<List<PlaylistItem>>() {
            @Override
            public void onResponse(Call<List<PlaylistItem>> call, Response<List<PlaylistItem>> response) {
                progressBar.setVisibility(View.GONE);
                if (response.isSuccessful() && response.body() != null) {
                    adapter.updateData(response.body());
                } else {
                    Toast.makeText(PlaylistDetailActivity.this, "Erro ao carregar itens", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<List<PlaylistItem>> call, Throwable t) {
                progressBar.setVisibility(View.GONE);
                Toast.makeText(PlaylistDetailActivity.this, "Falha de rede", Toast.LENGTH_SHORT).show();
            }
        });
    }
}
